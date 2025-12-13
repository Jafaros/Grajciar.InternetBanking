<script lang="ts">
	import './layout.css';
	import favicon from '$lib/assets/favicon.svg';
	import { APP_TITLE } from '$lib/config';

	let { children } = $props();
</script>

<svelte:head>
	<title>{APP_TITLE}</title>
	<link rel="icon" href={favicon} />
</svelte:head>
{@render children()}
