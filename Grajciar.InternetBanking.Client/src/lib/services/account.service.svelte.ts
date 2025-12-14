import { apiFetch } from '$lib/utils/fetch';
import { getContext, setContext } from 'svelte';
import { GetUserState } from './user.service.svelte';

export interface IAccount {
	id: number;
	balance: number;
	accountNumber: string;
	createdAt: string;
	userId: number;
	typeId: number;
	bankId: number;
}

class AccountService {
	private accounts = $state<IAccount[]>([]);

	constructor() {
		this.TryLoadAccounts();
	}

	private TryLoadAccounts = async () => {
		const userState = GetUserState();
		const user = userState.GetUser();

		if (user) {
			const response = await apiFetch(`/user/${user.id}/accounts`);

			const result = await response.json();
			if (response) {
				this.accounts = result.accounts as IAccount[];
			}
		}
	};

	public GetAccounts = () => {
		return this.accounts;
	};
}

const KEY = Symbol('ACCOUNT_KEY');

export function SetAccountState() {
	return setContext(KEY, new AccountService());
}

export function GetAccountState() {
	return getContext<ReturnType<typeof SetAccountState>>(KEY);
}
