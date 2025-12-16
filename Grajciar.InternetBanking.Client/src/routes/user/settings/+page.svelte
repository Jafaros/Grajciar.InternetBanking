<script lang="ts">
	import Spinner from '$lib/components/Spinner.svelte';
	import { GetUserState, type IUserUpdate } from '$lib/services/user.service.svelte';
	import { faAngleLeft } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { onMount } from 'svelte';
	import { fade } from 'svelte/transition';

	const userState = GetUserState();
	const user = $derived(userState.GetUser());

	let mounted = $state<boolean>(false);
	onMount(() => {
		mounted = true;
	});

	const formatedDate = $derived((date: string) => {
		const d = new Date(date);
		return `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}-${String(d.getDate()).padStart(2, '0')}T${String(d.getHours()).padStart(2, '0')}:${String(d.getMinutes()).padStart(2, '0')}`;
	});

	const formatedDateOnly = $derived((date: string) => {
		const d = new Date(date);
		return `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}-${String(d.getDate()).padStart(2, '0')}`;
	});

	let id = $derived(user?.id ?? 0);
	let username = $derived(user?.userName ?? '');
	let firstname = $derived(user?.firstName ?? '');
	let lastname = $derived(user?.lastName ?? '');
	let email = $derived(user?.email ?? '');
	let tel = $derived(user?.tel ?? '');
	let dateOfBirth = $derived(user?.dateOfBirth ?? '');
	let createdAt = $derived(user?.createdAt ?? '');
	let updatedAt = $derived(user?.updatedAt ?? '');

	let files = $state<FileList>();

	const Update = async () => {
		const updatedUser: IUserUpdate = {
			id,
			userName: username,
			firstName: firstname,
			lastName: lastname,
			email,
			tel
		};

		const file = files ? files[0] : undefined;
		await userState.UpdateUser(updatedUser, file);
	};

	let loading = $state<boolean>(false);
	const Submit = async () => {
		loading = true;

		await Update();

		loading = false;
	};
</script>

{#if mounted}
	<div in:fade>
		<button
			onclick={() => history.back()}
			class="flex cursor-pointer items-center gap-3 text-3xl text-white"
			><FontAwesomeIcon icon={faAngleLeft} class="text-3xl" /> Zpět</button
		>

		<h2 class="my-5 text-5xl font-semibold text-white max-md:text-3xl">Nastavení</h2>

		<form onsubmit={Submit} class="flex flex-1 flex-col gap-5 text-lg text-white">
			<div class="flex flex-col gap-2">
				<span>Uživatelské jméno</span>
				<input
					type="text"
					bind:value={username}
					class="flex-1 rounded-md border-white bg-slate-700"
					required
				/>
			</div>

			<div class="mb-8 flex w-full gap-8 max-md:flex-col max-sm:items-start">
				<div class="flex w-full flex-col justify-center gap-5">
					<div class="flex flex-col gap-1 max-sm:w-full">
						<span>Jméno</span>
						<input
							type="text"
							bind:value={firstname}
							class="rounded-md border-white bg-slate-700"
							required
						/>
					</div>
					<div class="flex flex-col gap-1 max-sm:w-full">
						<span>Příjmení</span>
						<input
							type="text"
							bind:value={lastname}
							class="rounded-md border-white bg-slate-700"
							required
						/>
					</div>
				</div>
				<div class="flex h-64 w-full flex-col items-end gap-3 max-md:items-start">
					<input
						type="file"
						bind:files
						class="w-full rounded-md border border-white bg-slate-700 p-3 text-lg"
					/>
					{#if files && files?.length > 0}
						<img
							src={URL.createObjectURL(files[0])}
							alt={user?.fullName}
							class="aspect-square w-[250px] rounded-full bg-slate-500 object-cover"
						/>
					{:else if user?.profileImagePath}
						<img
							src={user.profileImagePath}
							alt={user.fullName}
							class="aspect-square w-[250px] rounded-full bg-slate-500 object-cover"
						/>
					{:else}
						<div class="aspect-square w-[250px] rounded-full bg-slate-500"></div>
					{/if}
				</div>
			</div>

			<div class="flex w-full items-center gap-2 max-sm:flex-col max-sm:items-start">
				<div class="flex w-full flex-1 flex-col gap-2">
					<span>E-mail</span>
					<input
						type="email"
						bind:value={email}
						class="rounded-md border-white bg-slate-700"
						required
					/>
				</div>

				<div class="flex w-full flex-1 flex-col gap-2">
					<span>Telefonní číslo</span>
					<input
						type="tel"
						bind:value={tel}
						class="rounded-md border-white bg-slate-700"
						required
					/>
				</div>
			</div>

			<div class="flex flex-col gap-2">
				<span>Datum narození</span>
				<input
					type="date"
					value={formatedDateOnly(dateOfBirth ?? '')}
					disabled
					class="flex-1 rounded-md border-white bg-slate-700 disabled:border-gray-500 disabled:text-gray-500"
				/>
			</div>

			<div class="flex w-full items-center gap-2 max-sm:flex-col max-sm:items-start">
				<div class="flex flex-1 flex-col gap-1 max-sm:w-full">
					<span>Vytvořen dne</span>
					<input
						type="datetime-local"
						value={formatedDate(createdAt ?? '')}
						disabled
						class="rounded-md border-white bg-slate-700 disabled:border-gray-500 disabled:text-gray-500"
					/>
				</div>
				<div class="flex flex-1 flex-col gap-1 max-sm:w-full">
					<span>Upraven dne</span>
					<input
						type="datetime-local"
						value={formatedDate(updatedAt ?? '')}
						disabled
						class="rounded-md border-white bg-slate-700 disabled:border-gray-500 disabled:text-gray-500"
					/>
				</div>
			</div>

			<div class="flex items-center gap-2">
				<button
					type="submit"
					class="inline-flex flex-1 cursor-pointer items-center justify-center gap-3 rounded-lg bg-blue-500 px-5 py-3 font-semibold text-white"
				>
					{#if loading}
						<Spinner />
					{/if}
					Uložit
				</button>
			</div>
		</form>
	</div>
{/if}
