<script lang="ts">
	import { page } from '$app/state';
	import UserNavbar from '$lib/components/UserNavbar.svelte';
	import { GetCurrentUserPageLink } from '$lib/links';
	import { faBars } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { onMount } from 'svelte';
	import { fade, fly, slide } from 'svelte/transition';

	const { children } = $props();

	let mounted = $state<boolean>(false);
	onMount(() => {
		mounted = true;
	});

	let visible = $state<boolean>(false);
	const ToggleMenu = () => {
		visible = !visible;
	};
</script>

<svelte:head>
	<title>{GetCurrentUserPageLink(page.route.id ?? '')?.title ?? 'MindBank | Uživatel'}</title>
</svelte:head>

<div class="relative flex h-full h-screen items-stretch max-md:h-full">
	{#if mounted}
		<div class="relative w-[300px] max-md:hidden">
			<UserNavbar />
		</div>
	{/if}

	{#if visible}
		<!-- svelte-ignore a11y_no_static_element_interactions -->
		<!-- svelte-ignore a11y_click_events_have_key_events -->
		<div
			class="absolute top-0 left-0 z-100 flex h-full w-full"
			onclick={ToggleMenu}
			transition:fly={{ x: -300, duration: 200 }}
		>
			<div class="w-[250px]">
				<UserNavbar />
			</div>
			<div class="h-full w-full bg-black/70" transition:fade={{ duration: 200 }}></div>
		</div>
	{/if}

	<button
		type="button"
		onclick={ToggleMenu}
		class="absolute top-2 right-2 z-10 hidden size-10 rounded-lg bg-slate-500 max-md:block"
		><FontAwesomeIcon icon={faBars} class="text-2xl text-white" /></button
	>

	<div class="h-full min-h-screen w-full overflow-y-auto bg-slate-700 p-5 max-md:pt-16">
		{@render children()}
	</div>
</div>
