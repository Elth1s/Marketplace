import * as Yup from 'yup';

export const validationFields = Yup.object().shape({
    englishValue: Yup.string().min(2).max(30).required().label('English Value'),
    ukrainianValue: Yup.string().min(2).max(30).required().label('Ukrainian Value'),
    filterNameId: Yup.number().required().label('Filter name'),
    min: Yup.number().nullable().label('Min'),
    max: Yup.number().nullable().label('Max'),
});