import * as Yup from 'yup';

export const validationFields = Yup.object().shape({
    englishName: Yup.string().min(2).max(30).required().label('English Name'),
    ukrainianName: Yup.string().min(2).max(30).required().label('Ukrainian Name')
});