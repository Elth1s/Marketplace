import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import Rating from "@mui/material/Rating";
import Typography from "@mui/material/Typography";

import { PaperStyled, Img } from "../styled";

const iphone = "https://images.unsplash.com/photo-1607936854279-55e8a4c64888?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=111&q=128"
const data = [
    { image: iphone, title: "APPLE iPhone 12 64GB Blue", rating: 4.5, comment: 33, price: 26999, },
    { image: iphone, title: "APPLE iPhone 12 64GB Blue", rating: 4.5, comment: 33, price: 26999, },
    { image: iphone, title: "APPLE iPhone 12 64GB Blue", rating: 4.5, comment: 33, price: 26999, },
    { image: iphone, title: "APPLE iPhone 12 64GB Blue", rating: 4.5, comment: 33, price: 26999, },
    { image: iphone, title: "APPLE iPhone 12 64GB Blue", rating: 4.5, comment: 33, price: 26999, },
]

const Reviewed = () => {
    return (
        <>
            <Typography variant="h1" color="primary" sx={{ mb: "25px" }}>Reviewed products</Typography>
            <Grid container sx={{ ml: "66px", mr: "179px" }}>
                {data.map((item, index) => (
                    <Grid key={index} item sx={{ width: "420px", mr: "80px", mb: "43px" }}>
                        <PaperStyled sx={{ padding: "30px 41px" }}>
                            <Grid container>
                                <Grid item>
                                    <Box sx={{ width: 111, height: 128 }}>
                                        <Img alt={item.title} src={item.image} />
                                    </Box>
                                </Grid>
                                <Grid item xs={12} sm container sx={{ ml: "13px" }}>
                                    <Grid item xs container direction="column">
                                        <Grid item xs>
                                            <Typography variant="h6" sx={{ mb: "7px" }}>{item.title}</Typography>
                                            <Box sx={{ display: "flex", alignItems: "center" }}>
                                                <Rating defaultValue={item.rating} precision={0.5} readOnly sx={{ fontSize: "19px" }} />
                                                <Box sx={{ display: "flex", alignItems: "center", ml: "13px" }}>
                                                    <svg width="19" height="18" viewBox="0 0 19 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <g clip-path="url(#clip0_1058_7414)">
                                                            <path d="M15.8849 10.7862C15.8849 11.1715 15.7319 11.541 15.4594 11.8134C15.187 12.0859 14.8175 12.2389 14.4322 12.2389H5.71558L2.81006 15.1444V3.52234C2.81006 3.13705 2.96312 2.76753 3.23556 2.49508C3.50801 2.22264 3.87752 2.06958 4.26282 2.06958H14.4322C14.8175 2.06958 15.187 2.22264 15.4594 2.49508C15.7319 2.76753 15.8849 3.13705 15.8849 3.52234V10.7862Z" stroke="black" stroke-width="1.74332" stroke-linecap="round" stroke-linejoin="round" />
                                                        </g>
                                                        <defs>
                                                            <clipPath id="clip0_1058_7414">
                                                                <rect width="17.4332" height="17.4332" fill="white" transform="translate(0.630859 0.326172)" />
                                                            </clipPath>
                                                        </defs>
                                                    </svg>
                                                    <Typography variant="subtitle1" sx={{ ml: "3px" }}>{item.comment}</Typography>
                                                </Box>
                                            </Box>
                                        </Grid>
                                        <Grid item sx={{ display: "flex", alignItems: "center" }}>
                                            <Typography variant="h4" sx={{ fontWeight: "700" }}>{item.price}грн</Typography>
                                            <Button variant="contained" sx={{ minWidth: "auto", ml: "19px", p: "5px" }}>
                                                <svg width="19" height="18" viewBox="0 0 19 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                    <g clip-path="url(#clip0_1058_7419)">
                                                        <g clip-path="url(#clip1_1058_7419)">
                                                            <path d="M6.34503 15.6836C6.7462 15.6836 7.07142 15.3584 7.07142 14.9572C7.07142 14.556 6.7462 14.2308 6.34503 14.2308C5.94386 14.2308 5.61865 14.556 5.61865 14.9572C5.61865 15.3584 5.94386 15.6836 6.34503 15.6836Z" stroke="white" stroke-width="1.74332" stroke-linecap="round" stroke-linejoin="round" />
                                                            <path d="M14.3353 15.6836C14.7364 15.6836 15.0616 15.3584 15.0616 14.9572C15.0616 14.556 14.7364 14.2308 14.3353 14.2308C13.9341 14.2308 13.6089 14.556 13.6089 14.9572C13.6089 15.3584 13.9341 15.6836 14.3353 15.6836Z" stroke="white" stroke-width="1.74332" stroke-linecap="round" stroke-linejoin="round" />
                                                            <path d="M0.533936 0.429565H3.43946L5.38616 10.1558C5.45259 10.4902 5.63452 10.7906 5.90011 11.0044C6.1657 11.2182 6.49803 11.3318 6.83893 11.3253H13.8994C14.2402 11.3318 14.5726 11.2182 14.8382 11.0044C15.1038 10.7906 15.2857 10.4902 15.3521 10.1558L16.5143 4.06147H4.16584" stroke="white" stroke-width="1.74332" stroke-linecap="round" stroke-linejoin="round" />
                                                        </g>
                                                    </g>
                                                    <defs>
                                                        <clipPath id="clip0_1058_7419">
                                                            <rect width="17.4332" height="17.4332" fill="white" transform="translate(0.679199 0.13916)" />
                                                        </clipPath>
                                                        <clipPath id="clip1_1058_7419">
                                                            <rect width="17.4332" height="17.4332" fill="white" transform="translate(-0.192383 -0.296875)" />
                                                        </clipPath>
                                                    </defs>
                                                </svg>
                                            </Button>
                                        </Grid>
                                    </Grid>
                                </Grid>
                            </Grid>
                            <Box sx={{ display: "flex", justifyContent: "end" }}>
                                <Button variant="text"
                                    sx={{
                                        color: "#000",
                                        fontWeight: "500",
                                        mt: "22px",
                                        p: "0",
                                        textTransform: "lowercase",
                                        "&:hover": {
                                            backgroundColor: "transparent"
                                        }
                                    }}>
                                    remove
                                </Button>
                            </Box>
                        </PaperStyled>
                    </Grid>
                ))}
            </Grid>
        </>
    );
}

export default Reviewed;