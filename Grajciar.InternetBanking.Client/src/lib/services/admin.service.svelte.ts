import { apiFetch } from '$lib/utils/fetch';
import { getContext, onMount, setContext } from 'svelte';

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
}

export interface IBank {
	id: number;
	name: string;
	bankCode: string;
	address: string;
	swiftCode: string;
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

export const CARD_TYPES = [
	{ id: 0, name: 'Debetní' },
	{ id: 1, name: 'Kreditní' }
];

export const BANKACCOUNT_TYPES = [
	{ id: 1, name: 'Osobní' },
	{ id: 2, name: 'Spořící' },
	{ id: 3, name: 'Podnikatelský' },
	{ id: 4, name: 'Studentský' }
];

class AdminService {
	private admin = $state<IUser>();
	private users = $state<IUser[]>([]);
	private banks = $state<IBank[]>([]);

	constructor() {
		onMount(async () => {
			this.TryLoadAdmin();
			await this.FetchUsers();
			await this.FetchBanks();
		});
	}

	public SetAdmin = (user: IUser) => {
		this.admin = user;
		sessionStorage.setItem('admin', JSON.stringify(this.admin));
	};

	private SetUsers = (users: IUser[]) => {
		this.users = users;
	};

	public GetUsers = () => {
		return this.users;
	};

	public GetBanks = () => {
		return this.banks;
	};

	public GetUserById = (id: string) => {
		return this.users?.find((u) => u.id.toString() === id);
	};

	public SearchUsers = (query: string) => {
		return this.users?.filter(
			(u) =>
				u.fullName.toLocaleLowerCase().includes(query.toLocaleLowerCase()) ||
				u.userName.toLocaleLowerCase().includes(query.toLocaleLowerCase())
		);
	};

	public UpdateUser = async (user: IUser) => {
		const response = await apiFetch(`/admin/user/${user.id}`, {
			method: 'PATCH',
			body: JSON.stringify(user)
		});

		const result = await response.json();
		if (response.ok) {
			await this.FetchUsers();
		} else {
			return result.errors;
		}
	};

	public CreateBank = async (
		name: string,
		address: string,
		bankCode: string,
		swiftCode: string
	) => {
		const response = await apiFetch('/admin/bank', {
			method: 'POST',
			body: JSON.stringify({ name, address, bankCode, swiftCode })
		});

		if (response.ok) {
			await this.FetchBanks();
		}
	};

	public UpdateBank = async (
		id: number,
		name: string,
		address: string,
		bankCode: string,
		swiftCode: string
	) => {
		const response = await apiFetch(`/admin/bank/${id}`, {
			method: 'PATCH',
			body: JSON.stringify({ name, address, bankCode, swiftCode })
		});

		if (response.ok) {
			await this.FetchBanks();
		}
	};

	public FetchUsers = async () => {
		const response = await apiFetch('/admin/user', {
			credentials: 'include'
		});

		const result = await response.json();
		if (response.ok) {
			this.SetUsers(result);
		}
	};

	public FetchBanks = async () => {
		const response = await apiFetch('/admin/bank', {
			credentials: 'include'
		});

		const result = await response.json();
		if (response.ok) {
			this.banks = result;
		}
	};

	public FetchAccountsForUser = async (id: string): Promise<IAccount[]> => {
		const response = await apiFetch(`/admin/account/users/${id}`, {
			credentials: 'include'
		});

		const result = await response.json();
		if (response.ok) {
			return result;
		}

		return [];
	};

	public FetchAccount = async (id: string): Promise<IAccount | undefined> => {
		const response = await apiFetch(`/admin/account/${id}`);

		const result = await response.json();
		if (response.ok) {
			return result;
		}
	};

	public FetchAccounts = async (): Promise<IAccount[]> => {
		const response = await apiFetch(`/admin/account`);

		const result = await response.json();
		if (response.ok) {
			return result;
		}

		return [];
	};

	public CreateAccount = async (
		accountNumber: string,
		balance: number,
		typeId: string,
		bankId: string,
		userId: string
	) => {
		const response = await apiFetch(`/admin/account/users/${userId}/account`, {
			method: 'POST',
			body: JSON.stringify({ accountNumber, bankId, typeId, balance })
		});

		if (response.ok) {
			return true;
		}

		return false;
	};

	public UpdateAccount = async (
		id: number,
		accountNumber: string,
		bankId: number,
		typeId: number
	) => {
		const response = await apiFetch(`/admin/account/${id}`, {
			method: 'PATCH',
			body: JSON.stringify({ accountNumber, bankId, typeId })
		});

		if (response.ok) {
			return true;
		}

		return false;
	};

	public FetchCardsForAccount = async (id: string) => {
		const response = await apiFetch(`/admin/card/accounts/${id}/cards`);

		const result = await response.json();
		if (response.ok) {
			return result;
		}
	};

	public CreateCard = async (
		accountId: string,
		cardNumber: string,
		typeId: string,
		expirationDate: string,
		securityCode: string,
		isBlocked: boolean
	) => {
		const response = await apiFetch(`/admin/card/accounts/${accountId}/cards`, {
			method: 'POST',
			body: JSON.stringify({
				cardNumber,
				typeId,
				expirationDate,
				securityCode,
				isBlocked,
				accountId
			})
		});

		if (response.ok) {
			return true;
		}

		return false;
	};

	public UpdateCard = async (
		accountId: string,
		cardNumber: string,
		typeId: string,
		expirationDate: string,
		securityCode: string,
		isBlocked: boolean
	) => {
		return false;
	};

	public Logout = async (): Promise<boolean> => {
		const response = await apiFetch('/security/account/logout', {
			method: 'POST'
		});

		if (response.ok) {
			this.admin = undefined;
			sessionStorage.clear();
			return true;
		}

		return false;
	};

	private TryLoadAdmin = () => {
		const userData = sessionStorage.getItem('admin');

		if (userData) {
			this.admin = JSON.parse(userData) as IUser;
		}
	};

	public GetAdmin = () => {
		return this.admin;
	};
}

const KEY = Symbol('ADMIN_KEY');

export function SetAdminState() {
	return setContext(KEY, new AdminService());
}

export function GetAdminState() {
	return getContext<ReturnType<typeof SetAdminState>>(KEY);
}
