export const GetGreeting = () => {
	const now = new Date();
	const hours = now.getHours();

	let message = '';

	if (hours >= 6 && hours < 12) {
		message = 'Dobré ráno';
	} else if (hours >= 12 && hours < 18) {
		message = 'Dobré odpoledne';
	} else if (hours >= 18 || hours < 6) {
		message = 'Dobrý večer';
	}

	return message;
};
