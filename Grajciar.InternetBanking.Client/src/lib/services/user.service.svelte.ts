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

class UserService {
	private user = $state<IUser>();

	constructor() {
		onMount(() => this.TryLoadUser());
	}

	public SetUser = (user: IUser) => {
		this.user = user;
		sessionStorage.setItem('user', JSON.stringify(this.user));
	};

	public Logout = async (): Promise<boolean> => {
		const response = await apiFetch('/security/account/logout', {
			method: 'POST'
		});

		if (response.ok) {
			this.user = undefined;
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

	public GetUser = () => {
		return this.user;
	};

	public isLoggedIn = () => {
		return !!this.user;
	};
}

const KEY = Symbol('USER_KEY');

export function SetUserState() {
	return setContext(KEY, new UserService());
}

export function GetUserState() {
	return getContext<ReturnType<typeof SetUserState>>(KEY);
}
