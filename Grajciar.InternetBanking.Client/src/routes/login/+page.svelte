<script lang="ts">
	import { goto } from '$app/navigation';
	import Spinner from '$lib/components/Spinner.svelte';
	import { GetUserState, type IUser } from '$lib/services/user.service.svelte';
	import { apiFetch } from '$lib/utils/fetch';
	import { fly } from 'svelte/transition';
	import logo from '$lib/assets/logo.png';
	import { GetAdminState } from '$lib/services/admin.service.svelte';

	let email = $state<string>('petr.grajciar@gmail.com'); // petr.grajciar@gmail.com   admin@admin.cz
	let password = $state<string>('12345');
	let errorMessage = $state<string>('');

	let loading = $state<boolean>(false);

	const userState = GetUserState();
	const adminState = GetAdminState();

	const Submit = async () => {
		loading = true;

		const response = await apiFetch('/security/account/login', {
			method: 'POST',
			body: JSON.stringify({ email, password })
		});

		const result = await response.json();

		if (!result.ok && !result.success) {
			errorMessage = result.errorMessage;
		}

		if (response.ok) {
			errorMessage = '';

			if (result.user?.roles.includes('Admin')) {
				adminState.SetAdmin(result.user as IUser);
				await goto('/admin', { replaceState: true });
			} else if (result.user?.roles.includes('Manager')) {
				await goto('/manager', { replaceState: true });
			} else {
				userState.SetUser(result.user as IUser);
				await goto('/user', { replaceState: true });
			}
		}

		loading = false;
	};
</script>

<div class="flex h-screen items-center justify-center bg-gray-100">
	<form
		onsubmit={Submit}
		class="flex min-w-1/4 flex-col gap-5 rounded-lg bg-white p-8 p-16 shadow-md"
	>
		<a href="/" class="mx-auto">
			<img src={logo} alt="MindBank" class="w-16" />
		</a>
		<div class="flex flex-col gap-1">
			<span class="text-slate-500">E-mail</span>
			<input
				type="email"
				bind:value={email}
				class="rounded rounded-lg border border-slate-400"
				required
			/>
		</div>

		<div class="flex flex-col gap-1">
			<span class="text-slate-500">Heslo</span>
			<input
				type="password"
				bind:value={password}
				class="rounded rounded-lg border border-slate-400"
				required
			/>
		</div>

		{#if errorMessage}
			<p class="text-center text-red-500" in:fly={{ x: -20 }}>{errorMessage}</p>
		{/if}

		<button
			type="submit"
			class="flex cursor-pointer items-center justify-center gap-3 rounded-lg bg-blue-500 px-5 py-3 font-semibold text-white"
		>
			{#if loading}
				<Spinner />
			{/if}
			Přihlásit se
		</button>
	</form>
</div>
