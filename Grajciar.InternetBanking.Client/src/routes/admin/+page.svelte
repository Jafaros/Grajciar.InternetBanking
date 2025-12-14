<script lang="ts">
	import Card from '$lib/components/Card.svelte';
	import type { IAccount } from '$lib/services/account.service.svelte';
	import { GetAdminState } from '$lib/services/admin.service.svelte';
	import { GetGreeting } from '$lib/services/greeting.service';
	import { onMount } from 'svelte';
	import { fade } from 'svelte/transition';

	const adminState = GetAdminState();
	const admin = $derived(adminState.GetAdmin());

	let accounts = $state<IAccount[]>([]);
	onMount(async () => {
		accounts = await adminState.FetchAccounts();
	});

	const bankMoney = $derived(() => {
		return accounts.reduce((sum, a) => {
			sum += a.balance;
			return sum;
		}, 0);
	});

	let mounted = $state<boolean>(false);
	onMount(() => (mounted = true));
</script>

{#if mounted}
	<div in:fade>
		<div class="flex items-center justify-between">
			<div>
				<div class="text-3xl font-bold text-white">{GetGreeting()}, {admin?.fullName}</div>
				<p class="mt-1 text-xl text-white/50">Vítejte v administraci</p>
			</div>
		</div>

		<div
			class="mt-8 grid grid-cols-4 gap-3 max-lg:grid-cols-3 max-md:grid-cols-2 max-sm:grid-cols-1"
		>
			<Card bg={'bg-slate-500'} color={'text-white'}>
				<span class="text-lg uppercase">Celkový počet klientů</span>
				<div class="text-3xl">{adminState.GetUsers().length.toLocaleString()}</div>
			</Card>

			<Card bg={'bg-slate-500'} color={'text-white'}>
				<span class="text-lg uppercase">Počet registrovaných bank</span>
				<div class="text-3xl">{adminState.GetBanks().length.toLocaleString()}</div>
			</Card>

			<Card bg={'bg-slate-500'} color={'text-white'}>
				<span class="text-lg uppercase">Celkem peněz v bankách</span>
				<div class="text-3xl font-bold">{bankMoney().toLocaleString()} Kč</div>
			</Card>
		</div>
	</div>
{/if}
