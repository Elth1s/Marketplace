import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Box from "@mui/system/Box";

import { useTranslation } from 'react-i18next';
import { useTypedSelector } from "../../../../../hooks/useTypedSelector";
import { useActions } from "../../../../../hooks/useActions";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ShowMoreButton } from "../../../Catalog/styled";
import { CachedOutlined, StarRounded } from "@mui/icons-material";


const Reviews = () => {
    const { t } = useTranslation();

    const { shopReviews, shopReviewsCount } = useTypedSelector(state => state.shopPage);
    const { GetShopReviews, GetMoreShopReviews } = useActions();

    let { shopId } = useParams();

    const [page, setPage] = useState<number>(1);
    const [rowsPerPage, setRowsPerPage] = useState<number>(4);

    useEffect(() => {

        getData();
    }, [])

    const getData = async () => {
        if (!shopId)
            return;
        try {
            await GetShopReviews(shopId, page, rowsPerPage)
        } catch (ex) {
        }
    };

    const showMore = async () => {
        if (!shopId)
            return;
        try {
            let newPage = page + 1;
            await GetMoreShopReviews(shopId, newPage, rowsPerPage)
            setPage(newPage);
        } catch (ex) {

        }
    }
    return (
        <>
            {shopReviews.map((item, index) => (
                <Grid
                    key={index}
                    sx={{
                        background: "#FFFFFF",
                        border: "1px solid #7E7E7E",
                        borderRadius: "10px",
                        padding: "36px 42px",
                        mb: "20px "
                    }}>
                    <Grid item container sx={{ justifyContent: "space-between" }}>
                        <Grid item sx={{ mb: "40px" }}>
                            <Typography variant="h1" sx={{ mb: "20px" }}>{item.fullName}</Typography>
                            <Box sx={{ display: "flex" }}>
                                <Typography variant="h4" sx={{ display: "flex", alignItems: "center", mr: "40px" }}>
                                    {t('pages.seller.reviews.service')}
                                    <StarRounded color="primary" sx={{ fontSize: "30px", mx: "10px" }} />
                                    {item.serviceQualityRating}
                                </Typography>
                                <Typography variant="h4" sx={{ display: "flex", alignItems: "center", mr: "40px" }}>
                                    {t('pages.seller.reviews.terms')}
                                    <StarRounded color="primary" sx={{ fontSize: "30px", mx: "10px" }} />
                                    {item.timelinessRating}
                                </Typography>
                                <Typography variant="h4" sx={{ display: "flex", alignItems: "center" }}>
                                    {t('pages.seller.reviews.information')}
                                    <StarRounded color="primary" sx={{ fontSize: "30px", mx: "10px" }} />
                                    {item.informationRelevanceRating}
                                </Typography>
                            </Box>
                        </Grid>
                        <Grid item>
                            <Typography variant="h5">{item.date}</Typography>
                        </Grid>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h4">{item.comment}</Typography>
                    </Grid>
                </Grid>
            ))}
            {shopReviews.length != shopReviewsCount && <Box sx={{ display: "flex", justifyContent: "center" }}>
                <ShowMoreButton onClick={showMore} startIcon={<CachedOutlined />}>
                    {t("pages.catalog.showMore")}
                </ShowMoreButton>
            </Box>}
        </>
    );
}

export default Reviews;