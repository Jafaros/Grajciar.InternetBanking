<script lang="ts">
	import { page } from '$app/state';
	import UserNavbar from '$lib/components/UserNavbar.svelte';
	import { GetCurrentUserPageLink } from '$lib/links';
	import { onMount } from 'svelte';

	const { children } = $props();

	let mounted = $state<boolean>(false);
	onMount(() => (mounted = true));
</script>

<svelte:head>
	<title>{GetCurrentUserPageLink(page.route.id ?? '')?.title}</title>
</svelte:head>

<div class="flex h-screen items-stretch">
	{#if mounted}
		<div class="relative w-1/5">
			<UserNavbar />
		</div>
	{/if}

	<div class="w-full bg-slate-700 p-5">
		{@render children()}
	</div>
</div>
