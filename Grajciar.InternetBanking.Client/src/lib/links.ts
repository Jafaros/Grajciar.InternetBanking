import { faHome } from '@fortawesome/free-regular-svg-icons';
import {
	faArrowRightToBracket,
	faBank,
	faCog,
	faFileInvoice,
	faHouseUser,
	faUsers
} from '@fortawesome/free-solid-svg-icons';
import type { FontAwesomeIconProps } from '@fortawesome/svelte-fontawesome';

interface Link {
	title: string;
	name: string;
	slug: string;
	icon: FontAwesomeIconProps['icon'];
}

// Odkazy pro navigaci
export const NAVBAR_LINKS: Link[] = [
	{ title: 'MindBank', name: 'MindBank', slug: '/', icon: faHome },
	{
		title: 'MindBank - Přihlášení',
		name: 'Přihlášení',
		slug: '/login',
		icon: faArrowRightToBracket
	}
];

// Odkazy pro uživatelskou navigaci
export const USER_LINKS: Link[] = [
	{
		title: 'MindBank | Uživatel',
		name: 'Domů',
		slug: '/user',
		icon: faHouseUser
	},
	{
		title: 'MindBank | Účty',
		name: 'Účty',
		slug: '/user/accounts',
		icon: faFileInvoice
	},
	{
		title: 'MindBank | Nastavení',
		name: 'Nastavení',
		slug: '/user/settings',
		icon: faCog
	}
];

// Odkazy pro admin navigaci
export const ADMIN_LINKS: Link[] = [
	{
		title: 'MindBank | Admin',
		name: 'Domů',
		slug: '/admin',
		icon: faHouseUser
	},
	{
		title: 'MindBank | Admin | Uživatelé',
		name: 'Uživatelé',
		slug: '/admin/users',
		icon: faUsers
	},
	{
		title: 'MindBank | Admin | Banky',
		name: 'Banky',
		slug: '/admin/banks',
		icon: faBank
	}
];

export const GetCurrentUserPageLink = (slug: string) => {
	return USER_LINKS.find((l) => l.slug == slug);
};

export const GetCurrentAdminPageLink = (slug: string) => {
	return ADMIN_LINKS.find((l) => l.slug == slug);
};
