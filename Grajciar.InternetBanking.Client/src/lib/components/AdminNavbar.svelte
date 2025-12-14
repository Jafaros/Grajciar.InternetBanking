<script lang="ts">
	import { goto } from '$app/navigation';
	import { ADMIN_LINKS } from '$lib/links';
	import { GetAdminState } from '$lib/services/admin.service.svelte';
	import { faArrowRightFromBracket, faWallet } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';

	const adminState = GetAdminState();

	const Logout = async () => {
		const status = await adminState.Logout();
		if (status) {
			await goto('/login');
		}
	};
</script>

<div class="relative flex h-full flex-col bg-slate-800 p-5 shadow-md">
	<a href="/" class="flex items-center gap-5 text-center text-3xl font-semibold text-white">
		<FontAwesomeIcon icon={faWallet} class="text-4xl text-blue-200" />
		MindBank Admin
	</a>

	<div class="mt-8 flex h-full w-full flex-col gap-3">
		{#each ADMIN_LINKS as link}
			<a
				href={link.slug}
				class="flex items-center gap-3 rounded-lg px-4 py-2 text-xl text-white hover:bg-white/15"
				><FontAwesomeIcon icon={link.icon} class="text-2xl" /> {link.name}</a
			>
		{/each}

		<button
			type="button"
			onclick={Logout}
			class="mt-auto flex cursor-pointer items-center gap-3 rounded-lg px-4 py-2 text-xl text-white hover:bg-white/15"
		>
			<FontAwesomeIcon icon={faArrowRightFromBracket} class="text-2xl" />
			Odhlásit se
		</button>
	</div>
</div>
