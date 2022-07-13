import * as Yup from 'yup';

export const validationFields = Yup.object().shape({
    name: Yup.string().min(2).max(30).required().label('Name'),
    filterGroupId: Yup.number().required().label('Filter group'),
    unitId: Yup.number().nullable().label('Unit measure'),
});