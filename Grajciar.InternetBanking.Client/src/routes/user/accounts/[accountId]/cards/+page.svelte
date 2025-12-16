<script lang="ts">
	import { page } from '$app/state';
	import Card from '$lib/components/Card.svelte';
	import { CARD_TYPES } from '$lib/services/admin.service.svelte';
	import { GetUserState, type IAccount, type ICard } from '$lib/services/user.service.svelte';
	import { getCardGradient } from '$lib/utils/cardGradients';
	import { faCreditCard } from '@fortawesome/free-regular-svg-icons';
	import { faAngleLeft } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { onMount } from 'svelte';
	import { fade } from 'svelte/transition';

	const accountId = $derived(page.params.accountId);
	const userState = GetUserState();

	let account = $derived<IAccount | null>(userState.GetAccountById(accountId ?? ''));
	let cards = $state<ICard[]>([]);

	const formatedExpirationDate = (date: string) => {
		const d = new Date(date);
		return `${d.toLocaleDateString('cs-CZ', { month: '2-digit' })}/${d.toLocaleDateString('cs-CZ', { year: '2-digit' })}`;
	};

	const formatCardNumber = (number: string) => {
		return number
			.replace(/\s+/g, '')
			.replace(/(.{4})/g, '$1 ')
			.trim();
	};

	let mounted = $state<boolean>(false);
	onMount(async () => {
		mounted = true;
		cards = await userState.TryGetCardsForAccount(accountId ?? '');
	});
</script>

<svelte:head>
	<title>MindBank | {account?.type} účet</title>
</svelte:head>

{#if mounted}
	<div in:fade>
		<button
			onclick={() => history.back()}
			class="flex cursor-pointer items-center gap-3 text-3xl text-white"
			><FontAwesomeIcon icon={faAngleLeft} class="text-3xl" /> Zpět</button
		>

		<h2 class="my-5 text-5xl font-semibold text-white max-md:text-2xl">Bankovní karty</h2>

		<div class="grid grid-cols-3 gap-3 max-lg:grid-cols-1">
			{#each cards as card}
				<Card
					color="text-white"
					bg="bg-gradient-to-br ${getCardGradient(
						card
					)} shadow-2xl rounded-2xl min-w-128 max-sm:min-w-0"
				>
					<div class="relative flex h-48 flex-col p-5 max-sm:h-42 max-sm:p-3">
						<FontAwesomeIcon
							icon={faCreditCard}
							class="absolute top-4 right-4 text-2xl text-white/70 max-sm:top-2 max-sm:right-2"
						/>

						<div class="text-sm tracking-wider text-white/80 uppercase">
							{card.cardHolderName}
						</div>

						<div class="mt-6 text-3xl font-light tracking-widest max-sm:text-xl">
							{formatCardNumber(card.cardNumber)}
						</div>

						<div class="mt-auto flex items-end justify-between text-sm">
							<div class="flex flex-col gap-1">
								<span class="text-xs text-white/60">Type</span>
								<span class="text-lg font-medium">
									{CARD_TYPES[card.typeId].name}
								</span>
							</div>

							<div class="flex gap-5">
								<div class="flex flex-col items-end gap-1">
									<span class="text-xs text-white/60">Valid thru</span>
									<span class="text-lg">
										{formatedExpirationDate(card.expirationDate)}
									</span>
								</div>

								<div class="flex flex-col items-end gap-1">
									<span class="text-xs text-white/60">CVV</span>
									<span class="text-lg">
										{card.securityCode}
									</span>
								</div>
							</div>
						</div>
					</div>
				</Card>
			{/each}
		</div>
	</div>
{/if}
