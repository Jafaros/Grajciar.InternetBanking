<script lang="ts">
	import { page } from '$app/state';
	import {
		BANKACCOUNT_TYPES,
		GetAdminState,
		type IAccount,
		type IBank
	} from '$lib/services/admin.service.svelte';
	import { faClose } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { fade } from 'svelte/transition';

	const { account, onClose, onSuccess } = $props<{
		account: IAccount | null;
		onClose: () => void;
		onSuccess: () => void;
	}>();

	let id = $derived(account ? account.id : '');
	let accountNumber = $derived(account ? account.accountNumber : '');
	let balance = $derived(account ? account.balance : 0);
	let typeId = $derived(account ? account.typeId : '');
	let bankId = $derived(account ? account.bankId : '');

	let errors = $state<string[]>([]);

	const adminState = GetAdminState();
	let banks = $state<IBank[]>(adminState.GetBanks());

	const userId = $derived(page.params.id);

	const Create = async () => {
		const response = await adminState.CreateAccount(
			accountNumber,
			balance,
			typeId,
			bankId,
			Number(userId)
		);
		if (response.success) {
			onSuccess();
			onClose();
		} else {
			errors = response.errors;
		}
	};

	const Submit = async () => {
		await Create();
	};
</script>

<div
	class="fixed inset-0 z-50 flex items-center justify-center bg-black/50"
	in:fade={{ duration: 200 }}
>
	<div class="relative max-h-[95%] min-w-1/4 rounded-xl bg-slate-700 p-8">
		<button type="button" onclick={onClose} class="absolute top-4 right-3 cursor-pointer">
			<FontAwesomeIcon icon={faClose} class="text-2xl text-white" />
		</button>

		<h2 class="mb-5 text-3xl font-semibold text-white">
			{#if id}
				Upravit bankovní účet
			{:else}
				Vytvořit bankovní účet
			{/if}
		</h2>

		<form onsubmit={Submit} class="flex flex-col gap-3">
			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Číslo účtu</span>
				<input
					type="text"
					bind:value={accountNumber}
					class="rounded border border-white bg-slate-700 text-white"
					minlength="10"
					maxlength="26"
				/>
			</div>

			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Zůstatek</span>
				<input
					type="number"
					bind:value={balance}
					class="rounded border border-white bg-slate-700 text-white"
				/>
			</div>

			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Typ účtu</span>
				<select bind:value={typeId} class="rounded border border-white bg-slate-700 text-white">
					{#each BANKACCOUNT_TYPES as type}
						<option value={type.id}>{type.name}</option>
					{/each}
				</select>
			</div>

			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Banka</span>
				<select bind:value={bankId} class="rounded border border-white bg-slate-700 text-white">
					{#each banks as bank}
						<option value={bank.id}>{bank.name} - {bank.bankCode}</option>
					{/each}
				</select>
			</div>

			{#if errors.length}
				<ul class="text-sm text-red-600">
					{#each errors as error}
						<li>{error}</li>
					{/each}
				</ul>
			{/if}

			<button
				type="submit"
				class="cursor-pointer rounded bg-blue-500 px-5 py-3 text-lg font-semibold text-white"
				>Uložit</button
			>
		</form>
	</div>
</div>
