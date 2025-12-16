<script lang="ts">
	import { goto } from '$app/navigation';
	import { page } from '$app/state';
	import CardModal from '$lib/components/CardModal.svelte';
	import Spinner from '$lib/components/Spinner.svelte';
	import {
		BANKACCOUNT_TYPES,
		CARD_TYPES,
		GetAdminState,
		type IAccount,
		type ICard
	} from '$lib/services/admin.service.svelte';
	import { faEdit } from '@fortawesome/free-regular-svg-icons';
	import { faAngleLeft, faBalanceScale, faPlus } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { onMount } from 'svelte';
	import { fade, fly } from 'svelte/transition';

	const accountId = $derived(page.params.accountId);

	const adminState = GetAdminState();

	let mounted = $state<boolean>(false);
	let account = $state<IAccount | undefined>();
	let cards = $state<ICard[]>([]);

	onMount(async () => {
		mounted = true;
		account = await adminState.FetchAccount(accountId ?? '');
		cards = await adminState.FetchCardsForAccount(accountId ?? '');
	});

	let id = $derived(account ? account.id : 0);
	let accountNumber = $derived(account ? account.accountNumber : '');
	let balance = $derived(account ? account.balance : 0);
	let typeId = $derived(account ? account.typeId : 0);
	let bankId = $derived(account ? account.bankId : 0);
	let createdAt = $derived(account ? account.createdAt : '');

	let formatedDate = $derived((date: string) => {
		const d = new Date(date);
		return `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}-${String(d.getDate()).padStart(2, '0')}T${String(d.getHours()).padStart(2, '0')}:${String(d.getMinutes()).padStart(2, '0')}`;
	});

	let loading = $state<boolean>(false);
	const Submit = async () => {
		loading = true;

		if (await adminState.UpdateAccount(id, accountNumber, balance, bankId, typeId)) {
			account = await adminState.FetchAccount(accountId ?? '');
		}

		loading = false;
	};

	const Delete = async () => {
		if (await adminState.DeleteAccount(id)) {
			await goto(`/admin/users/${page.params.id}`, { replaceState: true });
		}
	};

	let selectedCard = $state<ICard | null>(null);
	let cardModalShown = $state<boolean>();

	const OpenModal = (card?: ICard) => {
		if (card) selectedCard = card;
		cardModalShown = true;
	};

	const CloseModal = () => {
		selectedCard = null;
		cardModalShown = false;
	};
</script>

{#if cardModalShown}
	<CardModal
		card={selectedCard}
		onClose={CloseModal}
		onSuccess={async () => {
			cards = await adminState.FetchCardsForAccount(accountId ?? '');
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
		<h2 class="my-4 text-5xl text-white">{account?.type} účet</h2>

		{#if account}
			<div class="flex gap-16 text-lg text-white max-lg:flex-col">
				<form class="flex flex-1 flex-col gap-5" onsubmit={Submit}>
					<div class="flex items-center gap-3 max-sm:flex-col max-sm:items-start">
						<div class="flex w-full flex-col gap-2">
							<span>Číslo účtu</span>
							<input
								type="text"
								bind:value={accountNumber}
								class="rounded-md border-white bg-slate-700"
								required
							/>
						</div>

						<div class="flex w-full flex-col gap-2">
							<span>Banka</span>
							<select bind:value={bankId} class="rounded-md border-white bg-slate-700" required>
								{#each adminState.GetBanks() as bank}
									<option value={bank.id}>{bank.name} - {bank.bankCode}</option>
								{/each}
							</select>
						</div>
					</div>

					<div class="flex flex-col gap-2">
						<span>Typ účtu</span>
						<select bind:value={typeId} class="rounded-md border-white bg-slate-700" required>
							{#each BANKACCOUNT_TYPES as type}
								<option value={type.id}>{type.name}</option>
							{/each}
						</select>
					</div>

					<div class="flex w-full flex-col gap-2">
						<span>Zůstatek</span>
						<input
							type="number"
							bind:value={balance}
							min="0"
							class="rounded-md border-white bg-slate-700 disabled:border-slate-500 disabled:text-slate-500"
						/>
					</div>

					<div class="flex w-full flex-col gap-2">
						<span>Založen dne</span>
						<input
							type="datetime-local"
							value={formatedDate(createdAt)}
							disabled
							class="rounded-md border-white bg-slate-700 disabled:border-slate-500 disabled:text-slate-500"
						/>
					</div>

					<div class="flex items-center gap-2">
						{#if id}
							<button
								type="button"
								onclick={Delete}
								class="flex flex-1 cursor-pointer items-center justify-center gap-3 rounded bg-red-500 px-5 py-3 text-white"
							>
								Odstranit
							</button>
						{/if}
						<button
							type="submit"
							class="flex flex-1 cursor-pointer items-center justify-center gap-3 rounded bg-blue-500 px-5 py-3 text-white"
						>
							{#if loading}
								<Spinner />
							{/if}
							Uložit
						</button>
					</div>
				</form>
				<div class="flex-1">
					<h3 class="text-2xl text-white">Bankovní karty</h3>

					<div class="mt-5 flex flex-col gap-3">
						{#each cards as card, i (card.id)}
							<div
								class="relative flex gap-5 rounded border border-white p-5 text-white transition hover:bg-white hover:text-slate-700 max-sm:flex-col max-sm:items-start"
								in:fly={{ x: 20, delay: i * 50 }}
							>
								<button
									type="button"
									class="cursor-pointer transition hover:scale-105"
									onclick={() => OpenModal(card)}
								>
									<FontAwesomeIcon icon={faEdit} class="text-xl" />
								</button>
								<div>
									<div>Držitel karty: {card.cardHolderName}</div>
									<div>Číslo karty: {card.cardNumber}</div>
									<div>Typ karty: {CARD_TYPES.find((c) => c.id === card.typeId)?.name}</div>
								</div>

								<div class="ml-auto max-sm:ml-0">
									<div>Datum expirace: {new Date(card.expirationDate).toLocaleDateString()}</div>
									<div>Bezpečnostní kód: {card.securityCode}</div>
								</div>
							</div>
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
		{/if}
	</div>
{/if}
