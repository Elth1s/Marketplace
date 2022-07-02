import * as Yup from 'yup';

export const countryValidation = Yup.object().shape({
    name: Yup.string().min(2).max(60).required().label('Name'),
    code: Yup.string().length(2).required().label('Code'),
});