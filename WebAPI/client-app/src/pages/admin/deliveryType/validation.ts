import * as Yup from 'yup';

export const fieldValidation = Yup.object().shape({
    englishName: Yup.string().min(2).max(25).label('English Name'),
    ukrainianName: Yup.string().min(2).max(25).label('Ukrainian Name'),
});