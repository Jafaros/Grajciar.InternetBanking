<script lang="ts">
	import logo from '$lib/assets/logo.png';
	import { GetUserState } from '$lib/services/user.service.svelte';

	const userState = GetUserState();
</script>

<div
	class="flex items-center justify-between gap-5 border border-b border-zinc-200 px-10 py-5 max-sm:px-5 max-sm:py-3"
>
	<a href="/">
		<img src={logo} alt="MindBank" class="w-14" />
	</a>

	{#if userState.isLoggedIn()}
		<div class="flex items-center gap-3">
			<a href="/user" class="rounded-lg bg-blue-500 px-6 py-3 font-semibold text-white">
				Otevřít účet
			</a>
		</div>
	{:else}
		<div class="flex items-center gap-3">
			<a href="/login" class="rounded-lg bg-blue-500 px-6 py-3 font-semibold text-white">
				Přihlásit se
			</a>
		</div>
	{/if}
</div>
