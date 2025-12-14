import { goto } from '$app/navigation';

export async function apiFetch(path: string, init?: RequestInit): Promise<Response> {
	const res = await fetch(`/api${path.startsWith('/') ? path : `/${path}`}`, {
		...init,
		credentials: 'include',
		headers: {
			'Content-Type': 'application/json',
			...init?.headers
		}
	});

	if (res.status === 401 || res.status === 403) {
		// eslint-disable-next-line svelte/no-navigation-without-resolve
		await goto('/login', { replaceState: true });
		throw new Error('Unauthorized');
	}

	return res;
}
