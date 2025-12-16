import { goto } from '$app/navigation';
import { resolve } from '$app/paths';

export async function apiFetch(path: string, init?: RequestInit): Promise<Response> {
	const res = await fetch(`/api${path.startsWith('/') ? path : `/${path}`}`, {
		...init,
		credentials: 'include',
		headers: {
			'Content-Type': 'application/json',
			...init?.headers
		}
	});

	if (
		(res.status === 401 || res.status === 403) &&
		(location.pathname.startsWith('/user') ||
			location.pathname.startsWith('/manager') ||
			location.pathname.startsWith('/admin'))
	) {
		// eslint-disable-next-line svelte/no-navigation-without-resolve
		sessionStorage.clear();
		await goto(resolve('/login'), { replaceState: true });
		throw new Error('Unauthorized');
	}

	return res;
}

export function parseAspNetErrors(errorResponse: { errors: string[] }): string[] {
	if (!errorResponse?.errors) return [];

	return Object.values(errorResponse.errors).flat().filter(Boolean) as string[];
}
