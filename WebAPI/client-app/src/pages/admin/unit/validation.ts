import * as Yup from 'yup';

export const UnitSchema = Yup.object().shape({
    englishMeasure: Yup.string().min(1).max(30).required().label('English Measure'),
    ukrainianMeasure: Yup.string().min(1).max(30).required().label('Ukrainian Measure'),
});