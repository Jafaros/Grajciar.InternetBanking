<script lang="ts">
	import { page } from '$app/state';
	import AdminNavbar from '$lib/components/AdminNavbar.svelte';
	import { GetCurrentAdminPageLink } from '$lib/links';
	import { onMount } from 'svelte';

	const { children } = $props();

	let mounted = $state<boolean>(false);
	onMount(() => (mounted = true));
</script>

<svelte:head>
	<title>{GetCurrentAdminPageLink(page.route.id ?? '')?.title ?? 'MindBank | Admin'}</title>
</svelte:head>

<div class="flex h-screen items-stretch">
	{#if mounted}
		<div class="relative w-1/5 max-lg:hidden">
			<AdminNavbar />
		</div>
	{/if}

	<div class="w-full overflow-y-auto bg-slate-700 p-5">
		{@render children()}
	</div>
</div>
