<script lang="ts">
	import { page } from '$app/state';
	import TransactionModal from '$lib/components/TransactionModal.svelte';
	import {
		GetUserState,
		type IAccount,
		type ITransaction
	} from '$lib/services/user.service.svelte';
	import { faCreditCard } from '@fortawesome/free-regular-svg-icons';
	import {
		faAngleLeft,
		faArrowDown,
		faArrowUp,
		faInfo,
		faPaperPlane
	} from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { onMount } from 'svelte';
	import { fade } from 'svelte/transition';

	const accountId = $derived(page.params.accountId);
	const userState = GetUserState();

	let account = $derived<IAccount | null>(userState.GetAccountById(accountId ?? ''));
	let transactions = $state<ITransaction[]>([]);

	let mounted = $state<boolean>(false);
	onMount(async () => {
		mounted = true;
		transactions = await userState.TryGetTransactionsForAccount(accountId ?? '');
		OrderTransactionsByDate();
	});

	const OrderTransactionsByDate = () => {
		transactions = [...transactions].sort(
			(a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
		);
	};

	let transitionModalShown = $state<boolean>(false);
	const OpenModal = () => {
		transitionModalShown = true;
	};

	const CloseModal = () => {
		transitionModalShown = false;
	};

	const OnSucces = async () => {
		await userState.TryLoadAccounts();
		transactions = await userState.TryGetTransactionsForAccount(accountId ?? '');
		OrderTransactionsByDate();
	};
</script>

<svelte:head>
	<title>MindBank | {account?.type} účet</title>
</svelte:head>

{#if transitionModalShown}
	<TransactionModal accountId={Number(accountId)} onClose={CloseModal} onSuccess={OnSucces} />
{/if}

{#if mounted}
	<div in:fade>
		<button
			onclick={() => history.back()}
			class="flex cursor-pointer items-center gap-3 text-3xl text-white"
			><FontAwesomeIcon icon={faAngleLeft} class="text-3xl" /> Zpět</button
		>

		<h2 class="my-5 text-5xl font-semibold text-white max-md:text-2xl">{account?.type} účet</h2>

		<div
			class="inline-flex truncate rounded-xl bg-slate-400 px-8 py-4 text-xl font-bold text-black/70 max-md:px-4 max-md:text-sm"
		>
			{account?.accountNumber} / {account?.bankCode}
		</div>

		<div
			class="my-5 w-full text-center text-6xl font-bold text-white max-md:text-left max-md:text-3xl"
		>
			{account?.balance.toLocaleString()}
			<span class="text-3xl font-semibold">Kč</span>
		</div>

		<div class="flex flex-wrap items-center gap-2">
			<button
				type="button"
				onclick={OpenModal}
				class="flex cursor-pointer items-center justify-center gap-2 rounded-full bg-slate-500 p-3 text-white transition hover:scale-105"
				><FontAwesomeIcon icon={faPaperPlane} class="text-xl" />Provést platbu</button
			>
			<a
				href="/user/accounts/{accountId}/cards"
				class="flex cursor-pointer items-center justify-center gap-2 rounded-full bg-slate-500 p-3 text-white transition hover:scale-105"
				><FontAwesomeIcon icon={faCreditCard} class="text-xl" />Zobrazit karty</a
			>
		</div>

		{#if transactions.length > 0}
			<div class="mt-8 flex flex-col gap-2 overflow-y-auto max-md:h-64">
				{#each transactions as t}
					<div
						class="relative flex flex-col gap-3 rounded-xl bg-slate-800/40 p-4 text-white shadow-md backdrop-blur
		   transition hover:bg-slate-800/60"
					>
						<div
							class="absolute top-4 left-3 flex h-8 w-8 items-center justify-center rounded-full
				{t.fromAccountId.toString() === accountId
								? 'bg-red-500/20 text-red-400'
								: 'bg-green-500/20 text-green-400'}"
						>
							<FontAwesomeIcon
								icon={t.fromAccountId.toString() === accountId ? faArrowUp : faArrowDown}
								class="text-lg"
							/>
						</div>

						<div class="flex items-center justify-between pl-10">
							<div class="flex flex-col">
								<div class="text-lg leading-tight font-medium">
									{t.description || 'Bez popisu'}
								</div>
								<div class="text-sm text-white/60">
									{new Date(t.createdAt).toLocaleString('cs-CZ', {
										day: 'numeric',
										month: 'numeric',
										year: 'numeric',
										hour: '2-digit',
										minute: '2-digit'
									})}
								</div>
							</div>

							<div class="text-right">
								<div
									class="text-xl font-semibold
				{t.fromAccountId.toString() === accountId ? 'text-red-400' : 'text-green-400'}"
								>
									{t.fromAccountId.toString() === accountId ? '-' : '+'}
									{t.amount.toLocaleString()} Kč
								</div>

								{#if t.status === 'SUCCESS'}
									<div class="text-sm text-green-500">Uhrazeno</div>
								{:else if t.status === 'PENDING'}
									<div class="text-sm text-yellow-500">Čeká</div>
								{:else}
									<div class="text-sm text-red-500">Selhala</div>
								{/if}
							</div>
						</div>

						{#if t.constantSymbol || t.variableSymbol}
							<div class="ml-10 rounded-lg bg-slate-900/40 p-3 text-sm text-white/80">
								<div class="mb-1 font-medium text-white/90">Detaily platby</div>

								{#if t.variableSymbol}
									<div>Variabilní symbol: <span class="font-mono">{t.variableSymbol}</span></div>
								{/if}

								{#if t.constantSymbol}
									<div>Konstantní symbol: <span class="font-mono">{t.constantSymbol}</span></div>
								{/if}
							</div>
						{/if}
					</div>
				{/each}
			</div>
		{/if}
	</div>
{/if}
