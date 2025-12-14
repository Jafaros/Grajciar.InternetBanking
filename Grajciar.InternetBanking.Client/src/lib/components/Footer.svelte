<script lang="ts">
	import logo from '$lib/assets/logo.png';
</script>

<div class="border-t border-zinc-200 px-10 py-8">
	<div class="flex justify-between">
		<img src={logo} alt="MindBank" class="w-14" />

		<div class="flex items-center gap-5 text-gray-500">
			<span>Zásady zpracování osobních údajů</span>
			<span>Podmínky služby</span>
			<span>Podpora</span>
		</div>
	</div>

	<div class="mt-10 flex justify-center gap-5 text-gray-500">
		<span>&copy; {new Date().getFullYear()} MindBank</span>
		<span>Všechna práva hrazena.</span>
	</div>
</div>
