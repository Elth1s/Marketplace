import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";

import StarIcon from '@mui/icons-material/Star';

import { useTranslation } from 'react-i18next';

import { RatingStyle } from "../../../../../components/Rating/styled";

import { FC } from "react";
import { BorderLinearProgress } from "../../styled";

interface IRatingProgres {
    ratingValue: number,
    ratingValueCount: number,
    allRatingCount: number
}

const RatingProgres: FC<IRatingProgres> = ({ ratingValue, ratingValueCount, allRatingCount }) => {
    return (
        <Box sx={{ display: 'flex', alignItems: 'center', mb: "65px" }}>
            <Box sx={{ display: 'flex', alignItems: 'center', mr: "10px" }}>
                <StarIcon color="primary" sx={{ fontSize: "30px", mr: "10px" }} />
                <Typography variant="h2">{ratingValue}</Typography>
            </Box>
            <BorderLinearProgress
                variant="determinate"
                value={ratingValueCount / allRatingCount * 100}
                sx={{ mr: "14px" }}
            />
            <Typography variant="h2">{ratingValueCount}</Typography>
        </Box>
    )
}

const About = () => {
    const { t } = useTranslation();

    return (
        <Grid
            container
            sx={{
                justifyContent: "space-between",
                mb: "150px"
            }}>
            <Grid item>
                <Typography variant="h1" sx={{ mb: "30px" }}>{t('pages.seller.about.rating.title')}</Typography>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: "20px" }}>
                    <RatingStyle readOnly value={5} precision={0.5} sx={{ fontSize: "30px", mr: "10px" }} />
                    <Typography variant="h2">{5} ({10}  {t('pages.seller.about.rating.assessments')})</Typography>
                </Box>
                <RatingProgres ratingValue={5} ratingValueCount={8} allRatingCount={10} />
                <RatingProgres ratingValue={4} ratingValueCount={1} allRatingCount={10} />
                <RatingProgres ratingValue={3} ratingValueCount={1} allRatingCount={10} />
                <RatingProgres ratingValue={2} ratingValueCount={0} allRatingCount={10} />
                <RatingProgres ratingValue={1} ratingValueCount={0} allRatingCount={10} />
            </Grid>
            <Grid item>
                <Typography variant="h1" sx={{ mb: "30px" }}>{t('pages.seller.about.assessment.title')}</Typography>
                <Box sx={{ bm: "65px" }}>
                    <Typography variant="h2" sx={{ mb: "15px" }}>{t('pages.seller.about.assessment.product')}</Typography>
                    <RatingStyle readOnly value={5} precision={0.5} sx={{ fontSize: "30px" }} />
                </Box>
                <Box sx={{ bm: "65px" }}>
                    <Typography variant="h2" sx={{ mb: "15px" }}>{t('pages.seller.about.assessment.delivery')}</Typography>
                    <RatingStyle readOnly value={4.5} precision={0.5} sx={{ fontSize: "30px" }} />
                </Box>
                <Box sx={{ bm: "65px" }}>
                    <Typography variant="h2" sx={{ mb: "15px" }}>{t('pages.seller.about.assessment.service')}</Typography>
                    <RatingStyle readOnly value={4.5} precision={0.5} sx={{ fontSize: "30px" }} />
                </Box>
            </Grid>
            <Grid item>
                <Typography variant="h2" sx={{ mb: "30px" }}>{t('pages.seller.about.schedule.title')}</Typography>
                <Typography variant="h2" sx={{ mb: "20px" }}>{t('pages.seller.about.schedule.work')}</Typography>
                <Typography variant="h2">{t('pages.seller.about.schedule.weekend')}</Typography>
            </Grid>
            <Grid item xs={12}>
                <Typography variant="h1" sx={{ mb: "30px" }}>{t('pages.seller.about.description.title')}</Typography>
                <Typography variant="h2">EUROSHOP presents a wide range of children's, women's and men's clothes of European quality, which provides maximum comfort and laconic design</Typography>
            </Grid>
        </Grid>
    );
}

export default About;