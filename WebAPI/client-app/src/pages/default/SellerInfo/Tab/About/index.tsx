import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";

import { useTranslation } from 'react-i18next';

import { RatingStyle } from "../../../../../components/Rating/styled";

import { FC, useEffect } from "react";
import { BorderLinearProgress } from "../../styled";
import { useParams } from "react-router-dom";
import { useTypedSelector } from "../../../../../hooks/useTypedSelector";
import { useActions } from "../../../../../hooks/useActions";
import { StarRounded } from "@mui/icons-material";

interface IRatingProgress {
    ratingValue: number,
    ratingValueCount: number,
    allRatingCount: number
}

const RatingProgress: FC<IRatingProgress> = ({ ratingValue, ratingValueCount, allRatingCount }) => {
    return (
        <Box sx={{ display: 'flex', alignItems: 'center', mb: "65px" }}>
            <Box sx={{ display: 'flex', alignItems: 'center', mr: "10px" }}>
                <StarRounded color="primary" sx={{ fontSize: "30px", mr: "10px" }} />
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

    const { shopPageInfo } = useTypedSelector(state => state.shopPage);
    const { GetShopInfo } = useActions();
    let { shopId } = useParams();

    useEffect(() => {
        getData();
    }, [shopId])

    const getData = async () => {
        if (!shopId)
            return;
        try {
            await GetShopInfo(shopId)
        } catch (ex) {
        }
    };

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
                    <RatingStyle
                        sx={{ fontSize: "30px", mr: "10px" }}
                        value={shopPageInfo.averageRating}
                        precision={0.1}
                        readOnly
                        icon={<StarRounded sx={{ fontSize: "30px" }} />}
                        emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                    />
                    <Typography variant="h2">{shopPageInfo.averageRating} ({shopPageInfo.countReviews}  {t('pages.seller.about.rating.assessments')})</Typography>
                </Box>
                {shopPageInfo.ratings.map((item, index) => (
                    <RatingProgress key={index} ratingValue={item.number} ratingValueCount={item.count} allRatingCount={shopPageInfo.countReviews} />
                ))}
            </Grid>
            <Grid item>
                <Typography variant="h1" sx={{ mb: "30px" }}>{t('pages.seller.about.assessment.title')}</Typography>
                <Box sx={{ bm: "65px" }}>
                    <Typography variant="h2" sx={{ mb: "15px" }}>{t('pages.seller.about.assessment.product')}</Typography>
                    <RatingStyle
                        sx={{ fontSize: "30px" }}
                        value={shopPageInfo.averageInformationRelevanceRating}
                        precision={0.1}
                        readOnly
                        icon={<StarRounded sx={{ fontSize: "30px" }} />}
                        emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                    />
                </Box>
                <Box sx={{ bm: "65px" }}>
                    <Typography variant="h2" sx={{ mb: "15px" }}>{t('pages.seller.about.assessment.delivery')}</Typography>
                    <RatingStyle
                        sx={{ fontSize: "30px" }}
                        value={shopPageInfo.averageTimelinessRating}
                        precision={0.1}
                        readOnly
                        icon={<StarRounded sx={{ fontSize: "30px" }} />}
                        emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                    />
                </Box>
                <Box sx={{ bm: "65px" }}>
                    <Typography variant="h2" sx={{ mb: "15px" }}>{t('pages.seller.about.assessment.service')}</Typography>
                    <RatingStyle
                        sx={{ fontSize: "30px" }}
                        value={shopPageInfo.averageServiceQualityRating}
                        precision={0.1}
                        readOnly
                        icon={<StarRounded sx={{ fontSize: "30px" }} />}
                        emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                    />
                </Box>
            </Grid>
            <Grid item>
                <Typography variant="h2" sx={{ mb: "30px" }}>{t('pages.seller.about.schedule.title')}</Typography>
                <Typography variant="h2" sx={{ mb: "20px" }}>{t('pages.seller.about.schedule.work')}</Typography>
                <Typography variant="h2">{t('pages.seller.about.schedule.weekend')}</Typography>
            </Grid>
            <Grid item xs={12}>
                <Typography variant="h1" sx={{ mb: "30px" }}>{t('pages.seller.about.description.title')}</Typography>
                <Typography variant="h2">{shopPageInfo.description}</Typography>
            </Grid>
        </Grid>
    );
}

export default About;