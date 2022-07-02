import * as Yup from 'yup';

export const validationFields = Yup.object().shape({
    name: Yup.string().min(2).max(50).label('Name'),
    parentId: Yup.number().nullable().label('Parent category'),
});