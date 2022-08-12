import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Box from "@mui/system/Box";

import StarIcon from '@mui/icons-material/Star';

const longText = "Нещодавно придбала срібне намисто у продавця EUROSHOP. Ціна дуже вигідна, матеріал якісний і дизайн лаконічний. Все надзвичайно сподобалось."

const data = [
    { name: "Марина", data: "12/12/2022", rating1: 4, rating2: 4, rating3: 4, desc: longText },
    { name: "Марина", data: "12/12/2022", rating1: 4, rating3: 4, desc: longText },
    { name: "Марина", data: "12/12/2022", rating1: 4, rating2: 4, rating3: 4, desc: longText },
]

const Reviews = () => {
    return (
        <>
            {data.map((item, index) => (
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
                            <Typography variant="h1" sx={{ mb: "20px" }}>{item.name}</Typography>
                            <Box sx={{ display: "flex" }}>
                                <Typography variant="h4" sx={{ display: "flex", alignItems: "center", mr: "40px" }}>
                                    Quality of service
                                    <StarIcon color="primary" sx={{ fontSize: "30px", mx: "10px" }} />
                                    {item.rating1}
                                </Typography>
                                <Typography variant="h4" sx={{ display: "flex", alignItems: "center", mr: "40px" }}>
                                    Observance of terms
                                    <StarIcon color="primary" sx={{ fontSize: "30px", mx: "10px" }} />
                                    {item.rating2}
                                </Typography>
                                <Typography variant="h4" sx={{ display: "flex", alignItems: "center" }}>
                                    Information relevance
                                    <StarIcon color="primary" sx={{ fontSize: "30px", mx: "10px" }} />
                                    {item.rating3}
                                </Typography>
                            </Box>
                        </Grid>
                        <Grid item>
                            <Typography variant="h5">{item.data}</Typography>
                        </Grid>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h4">{item.desc}</Typography>
                    </Grid>
                </Grid>
            ))}
        </>
    );
}

export default Reviews;