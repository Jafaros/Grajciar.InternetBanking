<script lang="ts">
	import { GetUserState, type ITransactionCreate } from '$lib/services/user.service.svelte';
	import { faClose } from '@fortawesome/free-solid-svg-icons';
	import { FontAwesomeIcon } from '@fortawesome/svelte-fontawesome';
	import { fade } from 'svelte/transition';

	const { accountId, onClose, onSuccess } = $props<{
		accountId: number | null;
		onClose: () => void;
		onSuccess: () => void;
	}>();

	const userState = GetUserState();

	let accId = $derived<number>(accountId ?? 0);
	let toAccountNumber = $state<string>('');
	let toBankCode = $state<string>('');
	let constantSymbol = $state<string>('');
	let variableSymbol = $state<string>('');
	let amount = $state<number>(0);
	let description = $state<string>('');

	let errors = $state<string[]>([]);

	const NewBalance = $derived(() => {
		const balance = userState.GetAccountBalance(accId.toString());
		if (!balance) return 0;
		return balance - amount;
	});

	const Create = async () => {
		const fromAccount = userState.GetAccountById(accId.toString());

		if (fromAccount) {
			const transaction: ITransactionCreate = {
				fromAccountNumber: fromAccount.accountNumber,
				fromBankCode: fromAccount.bankCode,
				toAccountNumber,
				toBankCode,
				description,
				amount,
				fromAccountId: accId,
				constantSymbol,
				variableSymbol
			};

			const response = await userState.CreateTransaction(transaction);

			if (response.success) {
				onSuccess();
				onClose();
			} else {
				errors = response.errors;
			}
		}
	};

	const Submit = async () => {
		await Create();
	};
</script>

<div
	class="fixed inset-0 z-50 flex items-center justify-center bg-black/50"
	in:fade={{ duration: 200 }}
>
	<div
		class="relative max-h-[95%] max-w-[95%] min-w-1/4 overflow-y-auto rounded-xl bg-slate-700 p-8"
	>
		<button type="button" onclick={onClose} class="absolute top-4 right-3 cursor-pointer">
			<FontAwesomeIcon icon={faClose} class="text-2xl text-white" />
		</button>

		<h2 class="mb-5 text-3xl font-semibold text-white">Provést platbu</h2>

		<form onsubmit={Submit} class="flex flex-col gap-3">
			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Z účtu</span>
				<select class="rounded border bg-slate-700 text-white" bind:value={accId} required>
					{#each userState.GetAccounts() as account}
						<option value={account.id}
							>{account.type} účet -
							{account.accountNumber} / {account.bankCode}</option
						>
					{/each}
				</select>
			</div>
			<div class="flex items-center gap-2 max-md:flex-col max-md:items-start">
				<div class="flex w-2/3 flex-col gap-2">
					<span class="text-lg text-white">Číslo účtu</span>
					<input
						type="text"
						class="rounded border bg-slate-700 text-white"
						bind:value={toAccountNumber}
						required
					/>
				</div>
				<div class="flex w-1/3 flex-col gap-2">
					<span class="text-lg text-white">Banka</span>
					<select
						class="rounded border bg-slate-700 text-white disabled:border-slate-500 disabled:text-slate-500"
						bind:value={toBankCode}
						required
					>
						{#each userState.GetBanks() as bank}
							<option value={bank.bankCode}>{bank.name}</option>
						{/each}
					</select>
				</div>
			</div>

			<div class="flex items-center gap-2 max-md:flex-col max-md:items-start">
				<div class="flex flex-col gap-2">
					<span class="text-lg text-white">Konstatní symbol</span>
					<input
						type="text"
						class="rounded border bg-slate-700 text-white"
						bind:value={constantSymbol}
					/>
				</div>
				<div class="flex flex-col gap-2">
					<span class="text-lg text-white">Variabilní symbol</span>
					<input
						type="text"
						class="rounded border bg-slate-700 text-white"
						bind:value={variableSymbol}
					/>
				</div>
			</div>

			<div class="flex flex-col gap-2">
				<span class="text-lg text-white">Popis platby</span>
				<textarea
					class="rounded border bg-slate-700 text-white"
					bind:value={description}
					maxlength="255"
					required
				></textarea>
			</div>

			<div class="flex items-end gap-2">
				<div class="flex w-2/3 flex-col gap-2">
					<span class="text-lg text-white">Částka</span>
					<input
						type="number"
						class="rounded border bg-slate-700 text-2xl text-white"
						bind:value={amount}
						required
					/>
				</div>
				<div class="flex w-1/3 flex-col gap-2 text-right text-white">
					<span class="text-sm">Nový zůstatek</span>
					<div class="text-xl">{NewBalance().toLocaleString()} Kč</div>
				</div>
			</div>

			{#if errors.length}
				<ul class="text-red-500">
					{#each errors as error}
						<li>{error}</li>
					{/each}
				</ul>
			{/if}

			<button
				type="submit"
				disabled={NewBalance() < 0}
				class="cursor-pointer rounded bg-blue-500 px-5 py-3 text-lg font-semibold text-white disabled:bg-gray-500"
				>Odeslat</button
			>
		</form>
	</div>
</div>
