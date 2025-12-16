<script lang="ts">
	import { faCheckCircle } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import hero_img from '$lib/assets/hero-img.jpg';
	import { onMount } from 'svelte';
	import { fade, fly } from 'svelte/transition';
	import Header from '$lib/components/Header.svelte';
	import Footer from '$lib/components/Footer.svelte';

	let mounted = $state<boolean>(false);
	onMount(() => (mounted = true));
</script>

{#if mounted}
	<Header />

	<div
		class="flex items-center gap-10 px-10 py-24 max-sm:px-5 max-sm:py-6"
		in:fade={{ duration: 200 }}
	>
		<div class="flex-1">
			<h1 class="text-7xl font-semibold text-slate-900 max-sm:text-5xl" in:fly={{ x: -100 }}>
				Používejte bankovnictví <span class="text-blue-500">bez hranic.</span>
			</h1>
			<p class="my-5 max-w-2xl text-xl text-slate-500" in:fly={{ x: -50, delay: 200 }}>
				Spravujte své finance odkudkoliv, bez zbytečných omezení. Bezpečné prostředí, rychlé
				transakce a plná kontrola nad každým pohybem vašich peněz.
			</p>

			<a
				href="/register"
				class="inline-block rounded-lg bg-blue-500 px-5 py-3 font-semibold text-white shadow-lg"
				>Založit nový účet</a
			>

			<div class="mt-10 flex items-center gap-5 text-gray-500 max-sm:flex-col max-sm:items-start">
				<span class="flex items-center gap-3"
					><FontAwesomeIcon icon={faCheckCircle} class="text-xl text-green-500" /> Žádná kreditní karta
					není třeba</span
				>
				<span class="flex items-center gap-3"
					><FontAwesomeIcon icon={faCheckCircle} class="text-xl text-green-500" /> Zrušte kdykoliv</span
				>
			</div>
		</div>

		<div class="flex-1 max-lg:hidden">
			<img src={hero_img} alt="" class="size-full max-h-[500px] rounded-xl object-cover" />
		</div>
	</div>

	<Footer />
{/if}
