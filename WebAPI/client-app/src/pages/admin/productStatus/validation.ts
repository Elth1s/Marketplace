import * as Yup from 'yup';

export const fieldValidation = Yup.object().shape({
    name: Yup.string().min(2).max(20).required().label('Name'),
});