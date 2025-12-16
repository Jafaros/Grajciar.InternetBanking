<script lang="ts">
	import { page } from '$app/state';
	import {
		CARD_TYPES,
		GetAdminState,
		type IAccount,
		type ICard
	} from '$lib/services/admin.service.svelte';
	import { faClose } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { onMount } from 'svelte';
	import { fade } from 'svelte/transition';

	const { card, onClose, onSuccess } = $props<{
		card: ICard | null;
		onClose: () => void;
		onSuccess: () => void;
	}>();

	let accounts = $state<IAccount[]>([]);
	onMount(async () => {
		accounts = await adminState.FetchAccountsForUser(page.params.id ?? '');
	});

	let id = $derived(card ? card.id : '');
	let cardNumber = $derived(card ? card.cardNumber : '');
	let expirationDate = $derived(card ? card.expirationDate : '');
	let securityCode = $derived(card ? card.securityCode : '');
	let isBlocked = $derived(card ? card.isBlocked : false);
	let typeId = $derived(card ? card.typeId : 0);
	let accountId = $derived(card ? card.accountId : '');

	let errors = $state<string[]>([]);

	const adminState = GetAdminState();

	const Create = async () => {
		const result = await adminState.CreateCard(
			accountId,
			cardNumber,
			typeId,
			expirationDate,
			securityCode,
			isBlocked
		);

		if (result.success) {
			onSuccess();
			onClose();
		} else {
			errors = result.errors;
		}
	};

	const Update = async () => {
		/*if (
			await adminState.UpdateCard(
				accountId,
				cardNumber,
				typeId,
				expirationDate,
				securityCode,
				isBlocked
			)
		)
			onSuccess();*/
	};

	const Delete = async () => {
		if (id) {
			if (await adminState.DeleteCard(id)) {
				onSuccess();
				onClose();
			}
		}
	};

	const Submit = async () => {
		if (id) {
			await Update();
		} else {
			await Create();
		}
	};
</script>

<div
	class="fixed inset-0 z-50 flex items-center justify-center bg-black/50"
	in:fade={{ duration: 200 }}
>
	<div
		class="relative max-h-[95%] min-w-1/4 overflow-y-auto rounded-xl bg-slate-700 p-8 max-sm:w-[95%]"
	>
		<button type="button" onclick={onClose} class="absolute top-4 right-3 cursor-pointer">
			<FontAwesomeIcon icon={faClose} class="text-2xl text-white" />
		</button>

		<h2 class="mb-5 text-3xl font-semibold text-white">
			{#if id}
				Upravit kartu
			{:else}
				Vytvořit kartu
			{/if}
		</h2>

		<form onsubmit={Submit} class="flex flex-col gap-3">
			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Číslo karty</span>
				<input
					type="text"
					class="rounded border border-white bg-slate-700 text-white"
					bind:value={cardNumber}
					maxlength="16"
					minlength="13"
					required
				/>
			</div>

			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Datum expirace</span>
				<input
					type="datetime-local"
					class="rounded border border-white bg-slate-700 text-white"
					bind:value={expirationDate}
					required
				/>
			</div>

			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Bezpečnostní kód</span>
				<input
					type="text"
					class="rounded border border-white bg-slate-700 text-white"
					bind:value={securityCode}
					maxlength="3"
					minlength="3"
					required
				/>
			</div>

			<div class="flex items-center gap-2">
				<span class="text-lg text-white">Zablokovaná</span>
				<input
					type="checkbox"
					class="size-5 rounded border border-white bg-slate-700 text-white"
					bind:checked={isBlocked}
				/>
			</div>

			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Typ karty</span>
				<select
					class="rounded border border-white bg-slate-700 text-white"
					bind:value={typeId}
					required
				>
					{#each CARD_TYPES as type, i}
						<option value={type.id}>{type.name}</option>
					{/each}
				</select>
			</div>

			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Bankovní účet uživatele</span>
				<select
					class="rounded border border-white bg-slate-700 text-white"
					bind:value={accountId}
					required
				>
					{#each accounts as account}
						<option value={account.id}>{account.accountNumber}/{account.bankCode}</option>
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

			<div class="flex items-center gap-2">
				{#if id}
					<button
						type="button"
						onclick={Delete}
						class="flex-1 cursor-pointer rounded bg-red-500 px-5 py-3 text-lg font-semibold text-white"
						>Odstranit</button
					>
				{/if}
				<!--<button
					type="submit"
					class="flex-1 cursor-pointer rounded bg-blue-500 px-5 py-3 text-lg font-semibold text-white"
					>Uložit</button
			>-->
			</div>
		</form>
	</div>
</div>

<style>
	input[type='datetime-local']::-webkit-calendar-picker-indicator {
		filter: invert(1);
	}
</style>
