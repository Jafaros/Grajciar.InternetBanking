<script lang="ts">
	import { page } from '$app/state';
	import BankAccountModal from '$lib/components/BankAccountModal.svelte';
	import Spinner from '$lib/components/Spinner.svelte';
	import { GetAdminState, type IAccount } from '$lib/services/admin.service.svelte';
	import type { IUser } from '$lib/services/user.service.svelte';
	import { faAngleLeft, faPlus } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { onMount } from 'svelte';
	import { fade, fly } from 'svelte/transition';

	let mounted = $state<boolean>(false);
	onMount(async () => {
		accounts = await adminState.FetchAccountsForUser(page.params.id ?? '');
		mounted = true;
	});

	const adminState = GetAdminState();
	const user = $derived(adminState.GetUserById(page.params.id ?? ''));
	let accounts = $state<IAccount[]>([]);

	let id = $derived(user?.id);
	let username = $derived(user?.userName);
	let firstname = $derived(user?.firstName);
	let lastname = $derived(user?.lastName);
	let email = $derived(user?.email);
	let tel = $derived(user?.tel);
	let dateOfBirth = $derived(user?.dateOfBirth);
	let createdAt = $derived(user?.createdAt);
	let updatedAt = $derived(user?.updatedAt);

	let formatedDate = $derived((date: string) => {
		const d = new Date(date);
		return `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}-${String(d.getDate()).padStart(2, '0')}T${String(d.getHours()).padStart(2, '0')}:${String(d.getMinutes()).padStart(2, '0')}`;
	});

	let formatedDateOnly = $derived((date: string) => {
		const d = new Date(date);
		return `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}-${String(d.getDate()).padStart(2, '0')}`;
	});

	let loading = $state<boolean>(false);
	let errors = $state<string[]>([]);

	const Submit = async () => {
		loading = true;

		const updateUser = {
			id,
			userName: username,
			firstName: firstname,
			lastName: lastname,
			email,
			tel,
			roles: user?.roles
		};

		errors = await adminState.UpdateUser(updateUser as IUser);
		loading = false;
	};

	let selectedAccount = $state<IAccount | null>(null);
	let accountModalShown = $state<boolean>();

	const OpenModal = (account?: IAccount) => {
		if (account) selectedAccount = account;
		accountModalShown = true;
	};

	const CloseModal = () => {
		selectedAccount = null;
		accountModalShown = false;
	};
</script>

{#if accountModalShown}
	<BankAccountModal
		account={selectedAccount}
		onClose={CloseModal}
		onSuccess={async () => {
			accounts = await adminState.FetchAccountsForUser(page.params.id ?? '');
		}}
	/>
{/if}

{#if mounted}
	<div in:fade>
		<button
			onclick={() => history.back()}
			class="flex cursor-pointer items-center gap-3 text-3xl text-white"
			><FontAwesomeIcon icon={faAngleLeft} class="text-3xl" /> Zpět</button
		>

		<h2 class="mt-4 text-3xl text-white">
			Uživatel <span class="font-semibold">{user?.fullName}</span>
		</h2>

		<div class="mt-8 flex gap-16 max-lg:flex-col">
			<form onsubmit={Submit} class="flex flex-1 flex-col gap-5 text-lg text-white">
				<div class="flex flex-col gap-2">
					<span>Uživatelské jméno</span>
					<input
						type="text"
						bind:value={username}
						class="flex-1 rounded-md border-white bg-slate-700"
						required
					/>
				</div>

				<div class="flex w-full items-center gap-2 max-sm:flex-col max-sm:items-start">
					<div class="flex flex-1 flex-col gap-1 max-sm:w-full">
						<span>Jméno</span>
						<input
							type="text"
							bind:value={firstname}
							class="rounded-md border-white bg-slate-700"
							required
						/>
					</div>
					<div class="flex flex-1 flex-col gap-1 max-sm:w-full">
						<span>Příjmení</span>
						<input
							type="text"
							bind:value={lastname}
							class="rounded-md border-white bg-slate-700"
							required
						/>
					</div>
				</div>

				<div class="flex flex-col gap-2">
					<span>E-mail</span>
					<input
						type="email"
						bind:value={email}
						class="flex-1 rounded-md border-white bg-slate-700"
						required
					/>
				</div>

				<div class="flex flex-col gap-2">
					<span>Telefonní číslo</span>
					<input
						type="tel"
						bind:value={tel}
						class="flex-1 rounded-md border-white bg-slate-700"
						required
					/>
				</div>

				<div class="flex flex-col gap-2">
					<span>Datum narození</span>
					<input
						type="date"
						value={formatedDateOnly(dateOfBirth ?? '')}
						disabled
						class="flex-1 rounded-md border-white bg-slate-700 disabled:border-gray-500 disabled:text-gray-500"
					/>
				</div>

				<div class="flex w-full items-center gap-2 max-sm:flex-col max-sm:items-start">
					<div class="flex flex-1 flex-col gap-1 max-sm:w-full">
						<span>Vytvořen dne</span>
						<input
							type="datetime-local"
							value={formatedDate(createdAt ?? '')}
							disabled
							class="rounded-md border-white bg-slate-700 disabled:border-gray-500 disabled:text-gray-500"
						/>
					</div>
					<div class="flex flex-1 flex-col gap-1 max-sm:w-full">
						<span>Upraven dne</span>
						<input
							type="datetime-local"
							value={formatedDate(updatedAt ?? '')}
							disabled
							class="rounded-md border-white bg-slate-700 disabled:border-gray-500 disabled:text-gray-500"
						/>
					</div>
				</div>

				<div class="flex flex-col gap-2">
					<span>Role</span>
					<div class="flex flex-col gap-2 rounded-md border border-white bg-slate-700 p-3">
						{#each user?.roles as role, i}
							<div class="rounded bg-slate-500 px-3 py-1">{role}</div>
						{/each}
					</div>
				</div>

				<button
					type="submit"
					class="inline-flex cursor-pointer items-center justify-center gap-3 rounded-lg bg-blue-500 px-5 py-3 font-semibold text-white"
				>
					{#if loading}
						<Spinner />
					{/if}
					Uložit
				</button>
			</form>

			<div class="flex-1">
				<h3 class="text-2xl text-white">Bankovní účty</h3>

				<div class="mt-5 flex flex-col gap-3">
					{#each accounts as account, i (account.id)}
						<a
							href="/admin/users/{page.params.id}/accounts/{account.id}"
							class="flex justify-between gap-5 rounded border border-white p-4 text-white transition hover:bg-white hover:text-slate-700"
							in:fly={{ x: 20, delay: i * 50 }}
						>
							<div>
								<div class="text-xl font-semibold">{account.type} účet</div>
								<div>{account.accountNumber}/{account.bankCode}</div>
							</div>
							<div class="text-right">
								<div class="uppercase">Zůstatek</div>
								<div class="text-2xl font-bold">{account.balance.toLocaleString()} Kč</div>
							</div>
						</a>
					{/each}

					<button
						type="button"
						class="cursor-pointer rounded border border-white p-4 text-white transition hover:bg-white hover:text-slate-700"
						onclick={() => OpenModal()}
					>
						<FontAwesomeIcon icon={faPlus} class="text-xl" />
					</button>
				</div>
			</div>
		</div>
	</div>
{/if}

<style>
	input[type='date']::-webkit-calendar-picker-indicator {
		filter: invert(1);
	}
</style>
