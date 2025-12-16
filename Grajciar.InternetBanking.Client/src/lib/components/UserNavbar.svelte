<script lang="ts">
	import { goto } from '$app/navigation';
	import { USER_LINKS } from '$lib/links';
	import { GetUserState } from '$lib/services/user.service.svelte';
	import { faArrowRightFromBracket, faWallet } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';

	const userState = GetUserState();

	const Logout = async () => {
		const status = await userState.Logout();
		if (status) {
			await goto('/login');
		}
	};
</script>

<div class="relative flex h-full flex-col bg-slate-800 p-5 shadow-md">
	<a href="/" class="flex items-center gap-5 text-3xl font-semibold text-white">
		<FontAwesomeIcon icon={faWallet} class="text-4xl text-blue-200" />
		MindBank
	</a>

	<div class="mt-8 flex h-full w-full flex-col gap-3">
		{#each USER_LINKS as link}
			<a
				href={link.slug}
				class="flex items-center gap-3 rounded-lg px-4 py-2 text-xl text-white hover:bg-white/15 max-md:px-2"
				><FontAwesomeIcon icon={link.icon} class="text-2xl" /> {link.name}</a
			>
		{/each}

		<button
			type="button"
			onclick={Logout}
			class="mt-auto flex cursor-pointer items-center gap-3 rounded-lg px-4 py-2 text-xl text-white hover:bg-white/15 max-md:px-2"
		>
			<FontAwesomeIcon icon={faArrowRightFromBracket} class="text-2xl" />
			Odhlásit se
		</button>
	</div>
</div>
