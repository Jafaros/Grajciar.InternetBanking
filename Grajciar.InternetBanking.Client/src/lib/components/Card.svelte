<script lang="ts">
	const { bg, color, children } = $props();
</script>

<div class="{bg} {color} w-full rounded-2xl p-5 transition-transform hover:scale-102">
	{@render children()}
</div>
