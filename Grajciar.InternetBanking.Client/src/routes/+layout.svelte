<script lang="ts">
	import './layout.css';
	import favicon from '$lib/assets/favicon.svg';
	import { APP_TITLE } from '$lib/config';
	import { SetUserState } from '$lib/services/user.service.svelte';
	import { SetAccountState } from '$lib/services/account.service.svelte';
	import { SetAdminState } from '$lib/services/admin.service.svelte';

	let { children } = $props();

	SetUserState();
	SetAdminState();
	SetAccountState();
</script>

<svelte:head>
	<title>{APP_TITLE}</title>
	<link rel="icon" href={favicon} />
</svelte:head>

{@render children()}
