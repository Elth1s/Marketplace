import * as Yup from 'yup';

export const fieldValidation = Yup.object().shape({
    englishName: Yup.string().min(2).max(20).required().label('English Name'),
    ukrainianName: Yup.string().min(2).max(20).required().label('Ukrainian Name'),
});