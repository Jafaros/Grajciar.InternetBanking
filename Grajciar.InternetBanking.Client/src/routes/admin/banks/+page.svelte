<script lang="ts">
	import BankModal from '$lib/components/BankModal.svelte';
	import { GetAdminState, type IBank } from '$lib/services/admin.service.svelte';
	import { faEdit } from '@fortawesome/free-regular-svg-icons';
	import { faPlus } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { onMount } from 'svelte';
	import { flip } from 'svelte/animate';
	import { fade, fly } from 'svelte/transition';

	let mounted = $state<boolean>(false);
	onMount(() => {
		mounted = true;
		adminState.FetchBanks();
	});

	const adminState = GetAdminState();
	const banks = $derived(adminState.GetBanks());

	let bankModalShown = $state<boolean>(false);
	let selectedBank = $state<IBank | null>(null);
</script>

{#if bankModalShown}
	<BankModal
		bank={selectedBank}
		onClose={() => {
			selectedBank = null;
			bankModalShown = false;
		}}
	/>
{/if}

{#if mounted}
	<div in:fade>
		<h2 class="text-5xl text-white">Banky</h2>

		<div class="mt-8 grid grid-cols-3 gap-8 max-lg:grid-cols-2 max-md:grid-cols-1 max-md:gap-3">
			{#each banks as bank, i (bank.id)}
				<div
					class="relative rounded-lg border border-white p-3 text-white"
					animate:flip
					in:fly={{ x: 20, delay: i * 50 }}
				>
					<button
						type="button"
						class="absolute top-2 right-2 cursor-pointer"
						onclick={() => {
							bankModalShown = true;
							selectedBank = bank;
						}}><FontAwesomeIcon icon={faEdit} class="text-xl" /></button
					>
					<div class="text-xl font-semibold">{bank.name}</div>
					<div class="">Adresa: {bank.address}</div>
					<div class="">Kód banky: {bank.bankCode}</div>
					<div class="">SWIFT: {bank.swiftCode}</div>
				</div>
			{/each}

			<button
				class="cursor-pointer rounded-lg border border-white p-3 text-white transition hover:bg-white hover:text-slate-700"
				onclick={() => {
					bankModalShown = true;
					selectedBank = null;
				}}
			>
				<FontAwesomeIcon icon={faPlus} />
			</button>
		</div>
	</div>
{/if}
