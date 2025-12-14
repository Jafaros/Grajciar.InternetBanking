<script lang="ts">
	import { GetAdminState } from '$lib/services/admin.service.svelte';
	import { onMount } from 'svelte';
	import { flip } from 'svelte/animate';
	import { fade, fly } from 'svelte/transition';

	const adminState = GetAdminState();

	let searchQuery = $state<string>('');
	let filteredUsers = $derived(() => {
		return adminState.SearchUsers(searchQuery);
	});

	let mounted = $state<boolean>(false);
	onMount(async () => {
		await adminState.FetchUsers();
		mounted = true;
	});
</script>

{#if mounted}
	<div in:fade>
		<h2 class="mb-4 text-5xl text-white">Uživatelé</h2>

		<div class="my-8 flex w-[300px] flex-col gap-2 text-white max-md:w-full">
			<span class="text-xl font-semibold">Vyhledat</span>
			<input
				type="search"
				bind:value={searchQuery}
				class="rounded-md border-white bg-slate-700 text-xl"
			/>
		</div>

		<div class="mb-8 rounded border-2 border-t border-white/50"></div>

		<div class="grid grid-cols-5 gap-2 max-lg:grid-cols-4 max-md:grid-cols-2 max-sm:grid-cols-1">
			{#each filteredUsers() as user, i (user.id)}
				<a
					animate:flip={{ duration: 200 }}
					href="/admin/users/{user.id}"
					class="w-full rounded-md border border-white p-5 text-white transition hover:bg-white hover:text-slate-700"
					in:fly={{ x: 20, delay: i * 50 }}
				>
					{user.fullName}
				</a>
			{/each}
		</div>
	</div>
{/if}
