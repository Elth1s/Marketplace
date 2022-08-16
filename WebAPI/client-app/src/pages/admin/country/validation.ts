import * as Yup from 'yup';

export const countryValidation = Yup.object().shape({
    englishName: Yup.string().min(2).max(60).required().label('English Name'),
    ukrainianName: Yup.string().min(2).max(60).required().label('Ukrainian Name'),
    code: Yup.string().length(2).required().label('Code'),
});