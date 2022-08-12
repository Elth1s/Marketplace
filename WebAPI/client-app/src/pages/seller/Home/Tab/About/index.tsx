import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid";
import Rating from "@mui/material/Rating";
import Box from "@mui/material/Box";

import StarIcon from '@mui/icons-material/Star';

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
    return (
        <Grid
            container
            sx={{
                justifyContent: "space-between",
                mb: "150px"
            }}>
            <Grid item>
                <Typography variant="h1" sx={{ mb: "30px" }}>Overall rating</Typography>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: "20px" }}>
                    <Rating readOnly value={5} precision={0.5} color="primary" sx={{ fontSize: "30px", mr: "10px" }} />
                    <Typography variant="h2">{5} ( {10} assessments)</Typography>
                </Box>
                <RatingProgres ratingValue={5} ratingValueCount={8} allRatingCount={10} />
                <RatingProgres ratingValue={4} ratingValueCount={1} allRatingCount={10} />
                <RatingProgres ratingValue={3} ratingValueCount={1} allRatingCount={10} />
                <RatingProgres ratingValue={2} ratingValueCount={0} allRatingCount={10} />
                <RatingProgres ratingValue={1} ratingValueCount={0} allRatingCount={10} />
            </Grid>
            <Grid item>
                <Typography variant="h1" sx={{ mb: "30px" }}>Assessment</Typography>
                <Box sx={{ bm: "65px" }}>
                    <Typography variant="h2" sx={{ mb: "15px" }}>Current information about the product</Typography>
                    <Rating readOnly value={5} precision={0.5} color="primary" sx={{ fontSize: "30px" }} />
                </Box>
                <Box sx={{ bm: "65px" }}>
                    <Typography variant="h2" sx={{ mb: "15px" }}>Compliance with delivery terms</Typography>
                    <Rating readOnly value={4.5} precision={0.5} color="primary" sx={{ fontSize: "30px" }} />
                </Box>
                <Box sx={{ bm: "65px" }}>
                    <Typography variant="h2" sx={{ mb: "15px" }}>Quality of service</Typography>
                    <Rating readOnly value={4.5} precision={0.5} color="primary" sx={{ fontSize: "30px" }} />
                </Box>
            </Grid>
            <Grid item>
                <Typography variant="h2" sx={{ mb: "30px" }}>Schedule of work</Typography>
                <Typography variant="h2" sx={{ mb: "20px" }}>Mon - Fri 08:00 - 21:00</Typography>
                <Typography variant="h2">Sat - NA weekend</Typography>
            </Grid>
            <Grid item xs={12}>
                <Typography variant="h1" sx={{ mb: "30px" }}>Store description</Typography>
                <Typography variant="h2">EUROSHOP presents a wide range of children's, women's and men's clothes of European quality, which provides maximum comfort and laconic design</Typography>
            </Grid>
        </Grid>
    );
}

export default About;