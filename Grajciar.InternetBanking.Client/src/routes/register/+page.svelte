<script lang="ts">
	import { goto } from '$app/navigation';
	import Spinner from '$lib/components/Spinner.svelte';
	import { GetUserState } from '$lib/services/user.service.svelte';
	import { apiFetch, parseAspNetErrors } from '$lib/utils/fetch';
	import { fly } from 'svelte/transition';
	import logo from '$lib/assets/logo.png';
	import { GetAdminState } from '$lib/services/admin.service.svelte';

	let userName = $state<string>('petrg');
	let firstName = $state<string>('Petr');
	let lastName = $state<string>('Grajciar');
	let email = $state<string>('petr.g@gmail.com'); // petr.grajciar@gmail.com
	let tel = $state<string>('123456789');
	let password = $state<string>('12345');
	let dateOfBirth = $state<string>('2003-09-26');
	let passwordRepeat = $state<string>('12345');
	let errors = $state<string[]>([]);

	const passwordsMatch = $derived(() => {
		return password == passwordRepeat;
	});

	let loading = $state<boolean>(false);

	const userState = GetUserState();
	const adminState = GetAdminState();

	const Submit = async () => {
		if (!passwordsMatch()) return;
		loading = true;

		const response = await apiFetch('/security/account/register', {
			method: 'POST',
			body: JSON.stringify({ userName, firstName, lastName, email, tel, password, dateOfBirth })
		});

		if (!response.ok) {
			const result = await response.json();
			errors = parseAspNetErrors(result);
		} else {
			errors = [];
			await goto('/login', { replaceState: true });
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
			<span class="text-slate-500">Uživatelské jméno</span>
			<input
				type="text"
				bind:value={userName}
				class="rounded rounded-lg border border-slate-400"
				minlength="5"
				required
			/>
		</div>

		<div class="flex items-center gap-3">
			<div class="flex flex-col gap-1">
				<span class="text-slate-500">Jméno</span>
				<input
					type="text"
					bind:value={firstName}
					class="rounded rounded-lg border border-slate-400"
					required
				/>
			</div>

			<div class="flex flex-col gap-1">
				<span class="text-slate-500">Příjmení</span>
				<input
					type="text"
					bind:value={lastName}
					class="rounded rounded-lg border border-slate-400"
					required
				/>
			</div>
		</div>

		<div class="flex items-center gap-3">
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
				<span class="text-slate-500">Telefonní číslo</span>
				<input
					type="tel"
					bind:value={tel}
					class="rounded rounded-lg border border-slate-400"
					required
				/>
			</div>
		</div>

		<div class="flex flex-col gap-1">
			<span class="text-slate-500">Datum narození</span>
			<input
				type="date"
				bind:value={dateOfBirth}
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

		<div class="flex flex-col gap-1">
			<span class="text-slate-500">Heslo znovu</span>
			<input
				type="password"
				bind:value={passwordRepeat}
				class="rounded rounded-lg border border-slate-400"
				required
			/>
		</div>

		{#if errors.length > 0}
			{#each errors as error}
				<p class="text-center text-red-500" in:fly={{ x: -20 }}>{error}</p>
			{/each}
		{/if}

		<button
			type="submit"
			disabled={!passwordsMatch()}
			class="flex cursor-pointer items-center justify-center gap-3 rounded-lg bg-blue-500 px-5 py-3 font-semibold text-white disabled:bg-slate-500"
		>
			{#if loading}
				<Spinner />
			{/if}
			Registrovat se
		</button>
	</form>
</div>
