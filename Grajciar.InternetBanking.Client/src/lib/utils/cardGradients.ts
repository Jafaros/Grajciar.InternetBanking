import type { ICard } from '$lib/services/user.service.svelte';

export const CARD_GRADIENTS = [
	'from-purple-700 via-indigo-600/40 to-slate-900',
	'from-fuchsia-700 via-purple-600/40 to-slate-900',
	'from-indigo-700 via-blue-600/40 to-slate-900',
	'from-violet-700 via-purple-800/40 to-slate-950',
	'from-sky-700 via-indigo-600/40 to-slate-900',
	'from-rose-700 via-fuchsia-600/40 to-slate-900'
];

const hashString = (value: string) => {
	let hash = 0;
	for (let i = 0; i < value.length; i++) {
		hash = (hash << 5) - hash + value.charCodeAt(i);
		hash |= 0;
	}
	return Math.abs(hash);
};

export const getCardGradient = (card: ICard) => {
	const key = card.cardNumber || card.id.toString();
	const index = hashString(key) % CARD_GRADIENTS.length;
	return CARD_GRADIENTS[index];
};
