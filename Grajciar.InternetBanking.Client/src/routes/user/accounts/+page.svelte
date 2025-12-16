<script lang="ts">
	import { GetUserState, type IAccount } from '$lib/services/user.service.svelte';
	import { faAngleLeft } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { onMount } from 'svelte';
	import { fade } from 'svelte/transition';

	const userState = GetUserState();
	let accounts = $derived<IAccount[]>(userState.GetAccounts());

	let mounted = $state<boolean>(false);
	onMount(async () => {
		mounted = true;
	});
</script>

{#if mounted}
	<div in:fade>
		<button
			onclick={() => history.back()}
			class="flex cursor-pointer items-center gap-3 text-3xl text-white"
			><FontAwesomeIcon icon={faAngleLeft} class="text-3xl" /> Zpět</button
		>

		<h2 class="my-5 text-5xl font-semibold text-white max-md:text-3xl">Bankovní účty</h2>

		<div class="grid grid-cols-3 gap-2 max-lg:grid-cols-2 max-md:grid-cols-1">
			{#each accounts as account}
				<a
					href="/user/accounts/{account.id}"
					class="flex flex-col gap-2 rounded-lg border border-white p-4 text-white transition hover:bg-white hover:text-slate-700"
				>
					<div class="text-3xl font-semibold max-md:text-xl">{account.type}</div>
					<div class="truncate text-xl">{account.accountNumber}/{account.bankCode}</div>
				</a>
			{/each}
		</div>
	</div>
{/if}
