import * as Yup from 'yup';

export const validationFields = Yup.object().shape({
    value: Yup.string().min(2).max(30).required().label('Value'),
    filterNameId: Yup.number().required().label('Filter name'),
    min: Yup.number().nullable().label('Min'),
    max: Yup.number().nullable().label('Max'),
});