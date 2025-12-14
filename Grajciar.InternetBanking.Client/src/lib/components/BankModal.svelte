<script lang="ts">
	import { GetAdminState, type IBank } from '$lib/services/admin.service.svelte';
	import { apiFetch } from '$lib/utils/fetch';
	import { faClose } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { fade } from 'svelte/transition';

	const { bank, onClose } = $props<{
		bank: IBank | null;
		onClose: () => void;
	}>();

	let id = $derived(bank ? bank.id : '');
	let name = $derived(bank ? bank.name : '');
	let address = $derived(bank ? bank.address : '');
	let bankCode = $derived(bank ? bank.bankCode : '');
	let swiftCode = $derived(bank ? bank.swiftCode : '');

	const adminState = GetAdminState();

	const Create = async () => {
		await adminState.CreateBank(name, address, bankCode, swiftCode);
	};

	const Update = async () => {
		await adminState.UpdateBank(id, name, address, bankCode, swiftCode);
	};

	const Submit = async () => {
		if (id) {
			await Update();
		} else {
			await Create();
		}

		onClose();
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
				Upravit banku
			{:else}
				Vytvořit banku
			{/if}
		</h2>

		<form onsubmit={Submit} class="flex flex-col gap-3">
			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Název banky</span>
				<input
					type="text"
					class="rounded border bg-slate-700 text-white"
					bind:value={name}
					required
				/>
			</div>
			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Adresa banky</span>
				<input
					type="text"
					class="rounded border bg-slate-700 text-white"
					bind:value={address}
					required
				/>
			</div>
			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Kód banky</span>
				<input
					type="text"
					class="rounded border bg-slate-700 text-white disabled:border-slate-500 disabled:text-slate-500"
					bind:value={bankCode}
					disabled={bank ? bank.bankCode : false}
					minlength="4"
					required
				/>
			</div>
			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">SWIFT kód</span>
				<input
					type="text"
					class="rounded border bg-slate-700 text-white"
					bind:value={swiftCode}
					required
				/>
			</div>

			<button
				type="submit"
				class="cursor-pointer rounded bg-blue-500 px-5 py-3 text-lg font-semibold text-white"
				>Uložit</button
			>
		</form>
	</div>
</div>
