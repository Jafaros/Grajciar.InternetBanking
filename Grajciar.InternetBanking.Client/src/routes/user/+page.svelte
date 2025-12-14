<script lang="ts">
	import { GetGreeting } from '$lib/services/greeting.service';
	import { GetUserState } from '$lib/services/user.service.svelte';
	import logo from '$lib/assets/logo.png';
	import Card from '$lib/components/Card.svelte';
	import { onMount } from 'svelte';
	import { fade } from 'svelte/transition';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';

	const userState = GetUserState();
	const user = $derived(userState.GetUser());

	let mounted = $state<boolean>(false);
	onMount(() => (mounted = true));
</script>

{#if mounted}
	<div in:fade>
		<div class="flex items-center justify-between">
			<div>
				<div class="text-3xl font-bold text-white">{GetGreeting()}, {user?.fullName}</div>
				<p class="mt-1 text-xl text-white/50">Vítejte ve svém uživatelském účtě</p>
			</div>

			<div>
				<img
					src={logo}
					alt="Profilová fotka"
					class="size-12 rounded-full border border-3 border-white object-cover"
				/>
			</div>
		</div>

		<div class="mt-5 grid grid-cols-3 gap-8">
			<Card bg={'bg-slate-300'} color={'text-black'}>
				<span class="text-lg uppercase">Celkový stav účtů</span>
				<div class="text-3xl font-bold">{Number(1_000_000).toLocaleString()} Kč</div>
			</Card>

			<Card bg={'bg-slate-500'} color={'text-white'}>
				<span class="text-xl font-semibold">Rychlé akce</span>
				<div class="mt-3 flex flex-wrap items-center gap-3">
					<div class="flex flex-col gap-2">
						<button
							type="button"
							class="flex aspect-square cursor-pointer items-center justify-center rounded-full bg-slate-700 p-3"
							><FontAwesomeIcon icon={faPaperPlane} class="text-2xl" /></button
						>
						Odeslat
					</div>
				</div>
			</Card>
		</div>
	</div>
{/if}
