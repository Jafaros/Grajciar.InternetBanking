import { goto } from '$app/navigation';
import { apiFetch } from '$lib/utils/fetch';
import { resolve } from '$app/paths';
import { getContext, onMount, setContext } from 'svelte';
import { page } from '$app/state';

export interface IUser {
	id: number;
	userName: string;
	firstName: string;
	lastName: string;
	email: string;
	tel?: string | null;
	dateOfBirth: string;
	createdAt: string;
	updatedAt?: string | null;
	fullName: string;
	roles: string[];
	profileImagePath?: string;
}

export interface IUserUpdate {
	id: number;
	userName: string;
	firstName: string;
	lastName: string;
	email: string;
	tel: string | null;
}

export interface IAccount {
	id: number;
	balance: number;
	accountNumber: string;
	createdAt: string;
	userId: number;
	typeId: number;
	bankId: number;
	bankCode: string;
	type: string;
}

export interface IBank {
	id: number;
	name: string;
	bankCode: string;
	address: string;
	swiftCode: string;
}

export interface BankAccountType {
	id: number;
	name: string;
}

export interface ICard {
	id: number;
	cardNumber: string;
	expirationDate: string;
	securityCode: string;
	cardHolderName: string;
	isBlocked: boolean;
	accountId: number;
	typeId: number;
}

export interface ITransaction {
	id: number;

	fromAccountNumber: string;
	fromBankCode: string;
	toAccountNumber: string;
	toBankCode: string;

	constantSymbol: string;
	variableSymbol: string;

	description: string;
	amount: number;
	createdAt: string;
	transactionType: string;
	status: string;

	fromAccountId: number;
	toAccountId: number;
}

export interface ITransactionCreate {
	fromAccountNumber: string;
	fromBankCode: string;
	toAccountNumber: string;
	toBankCode: string;

	constantSymbol?: string;
	variableSymbol?: string;

	description: string;
	amount: number;
	fromAccountId: number;
}

export const CARD_TYPES = [
	{ id: 0, name: 'Debetní' },
	{ id: 1, name: 'Kreditní' }
];

class UserService {
	private user = $state<IUser>();
	private accounts = $state<IAccount[]>([]);
	private banks = $state<IBank[]>([]);

	constructor() {
		onMount(async () => {
			this.TryLoadUser();

			const path = page.url.pathname;

			if (path.startsWith('/user')) {
				if (this.isLoggedIn() && this.user?.roles.includes('Customer')) {
					await this.TryLoadAccounts();
					await this.TryLoadBanks();
				} else {
					await goto(resolve('/login'), { replaceState: true });
				}
			}
		});
	}

	// User logic
	public SetUser = (user: IUser) => {
		this.user = user;
		sessionStorage.setItem('user', JSON.stringify(this.user));
		this.TryLoadAccounts();
		this.TryLoadBanks();
	};

	public UpdateUser = async (user: IUserUpdate, file?: File) => {
		const formData = new FormData();
		formData.append('userName', user.userName);
		formData.append('firstName', user.firstName);
		formData.append('lastName', user.lastName);
		formData.append('email', user.email);
		if (user.tel) formData.append('tel', user.tel);

		if (file) formData.append('profileImage', file);

		const response = await fetch(`/api/user/${user.id}`, {
			method: 'PATCH',
			body: formData
		});

		const result = await response.json();
		if (response.ok) {
			this.SetUser(result.user);
		}
	};

	public Logout = async (): Promise<boolean> => {
		const response = await apiFetch('/security/account/logout', {
			method: 'POST'
		});

		if (response.ok) {
			this.user = undefined;
			this.accounts = [];
			sessionStorage.clear();
			return true;
		}

		return false;
	};

	private TryLoadUser = () => {
		const userData = sessionStorage.getItem('user');

		if (userData) {
			this.user = JSON.parse(userData) as IUser;
		}
	};

	public Roles = () => {
		return this.GetUser()?.roles;
	};

	public GetUser = () => {
		return this.user;
	};

	private GetUserFromSession = () => {
		return JSON.parse(sessionStorage.getItem('user') ?? '') as IUser;
	};

	public isLoggedIn = () => {
		return !!this.user;
	};

	// Bank logic
	private TryLoadBanks = async () => {
		const response = await apiFetch(`/user/banks`);

		const result = await response.json();
		if (response.ok) {
			this.banks = result as IBank[];
		}
	};

	public GetBanks = () => {
		return this.banks;
	};

	// Account logic
	public TryLoadAccounts = async () => {
		const response = await apiFetch(`/user/${this.user?.id}/accounts`);

		const result = await response.json();
		if (response.ok) {
			this.accounts = result as IAccount[];
		}
	};

	public TryGetAccounts = async () => {
		const user = this.GetUserFromSession();
		if (user) {
			const response = await apiFetch(`/user/${user?.id}/accounts`);

			const result = await response.json();
			if (response.ok) {
				return result as IAccount[];
			} else {
				return [];
			}
		}

		return [];
	};

	public GetAllAccountsBalance = $derived(() => {
		return this.accounts
			? this.accounts.reduce((sum, a) => {
					return (sum += a.balance);
				}, 0)
			: 0;
	});

	public GetAccountBalance = (accountId: string) => {
		return this.GetAccountById(accountId)?.balance;
	};

	public GetAccounts = () => {
		return this.accounts;
	};

	public GetAccountById = (accountId: string) => {
		if (!accountId) return null;
		return this.accounts.find((a) => a.id.toString() === accountId) ?? null;
	};

	// Card logic
	public TryGetCardsForAccount = async (accountId: string) => {
		const user = this.GetUserFromSession();
		if (user && accountId) {
			const response = await apiFetch(`/user/${user?.id}/accounts/${accountId}/cards`);

			const result = await response.json();
			if (response.ok) {
				return result as ICard[];
			} else {
				return [];
			}
		}

		return [];
	};

	// Transactions logic
	public TryGetTransactionsForAccount = async (accountId: string) => {
		const user = this.GetUserFromSession();
		if (user && accountId) {
			const response = await apiFetch(`/user/${user?.id}/accounts/${accountId}/transactions`);

			const result = await response.json();
			if (response.ok) {
				return result as ITransaction[];
			} else {
				return [];
			}
		}

		return [];
	};

	public CreateTransaction = async (
		transaction: ITransactionCreate
	): Promise<{ success: boolean; errors: string[] }> => {
		const response = await apiFetch(`/user/${this.user?.id}/transaction`, {
			method: 'POST',
			body: JSON.stringify(transaction)
		});

		if (response.ok) {
			await this.TryGetTransactionsForAccount(transaction.fromAccountId.toString());
			return { success: true, errors: [] };
		} else {
			const result = await response.json();
			const errors = result;
			return { success: false, errors };
		}
	};
}

const KEY = Symbol('USER_KEY');

export function SetUserState() {
	return setContext(KEY, new UserService());
}

export function GetUserState() {
	return getContext<ReturnType<typeof SetUserState>>(KEY);
}
