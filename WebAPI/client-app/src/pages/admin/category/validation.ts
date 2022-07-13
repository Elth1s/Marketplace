import * as Yup from 'yup';

const urlSlugRegExp = /^[a-z0-9]+(?:-[a-z0-9]+)*$/


export const validationFields = Yup.object().shape({
    name: Yup.string().min(2).max(50).label('Name'),
    urlSlug: Yup.string().min(2).max(50).matches(urlSlugRegExp, 'Invalid format of  url slug').label('Url slug'),
    parentId: Yup.number().nullable().label('Parent category'),
});