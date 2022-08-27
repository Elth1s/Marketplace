import { StarRounded } from '@mui/icons-material'
import {
    Box,
    Button,
    IconButton,
    Typography
} from '@mui/material'
import { FC } from 'react'
import { useTranslation } from 'react-i18next'
import { dislike, like, link, reply } from '../../assets/icons'
import LinkRouter from '../LinkRouter'
import { RatingStyle } from '../Rating/styled'

interface Props {
    questionLink: string,
    fullName: string,
    date: string,
    message: string,
    images: Array<string>,
    isLiked: boolean,
    isDisliked: boolean,
    likes: number,
    dislikes: number,
    replies: number
}

const QuestionItem: FC<Props> = ({ questionLink, fullName, date, message, images, isLiked, isDisliked, likes, dislikes, replies }) => {
    const { t } = useTranslation();


    return (
        <Box sx={{ border: "1px solid #7e7e7e", borderRadius: "10px", p: "37px 41px", mb: "20px" }}>
            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                <Typography variant="h1">
                    {fullName}
                </Typography>
                <Box sx={{ display: "flex", alignItems: "center" }}>
                    <LinkRouter to={questionLink} underline="none" color="inherit">
                        <IconButton sx={{ "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}>
                            <img
                                style={{ width: "30px", height: "30px" }}
                                src={link}
                                alt="link"
                            />
                        </IconButton>
                    </LinkRouter>
                    <Typography variant="h5" sx={{ ml: "30px" }}>
                        {date}
                    </Typography>
                </Box>
            </Box>
            <Typography variant="h4" sx={{ mt: "21px" }}>
                {message}
            </Typography>
            {images.map((image, index) => {
                return (
                    <img
                        key={`review_image_${index}`}
                        style={{ width: "100px", height: "100px" }}
                        src={image}
                        alt="reviewImage"
                    />
                )
            })}
            <Box sx={{ display: "flex", justifyContent: "space-between", mt: "50px" }}>
                <Box>
                    <Button
                        sx={{
                            color: "inherit",
                            textTransform: "none",
                            fontSize: "18px",
                            mr: "40px",
                            "&:hover": { background: "transparent" },
                            "&& .MuiTouchRipple-child": { backgroundColor: "transparent" }
                        }}
                        startIcon={
                            <img
                                style={{ width: "30px", height: "30px" }}
                                src={reply}
                                alt="replyIcon"
                            />
                        }
                    >
                        {t("components.reviewItem.reply")}
                    </Button>
                    {replies != 0 && <Button
                        sx={{
                            textTransform: "none",
                            fontSize: "18px",
                            "&:hover": { background: "transparent" },
                            "&& .MuiTouchRipple-child": { backgroundColor: "transparent" }
                        }}
                    >
                        {t("components.reviewItem.showReply")} ({replies})
                    </Button>}
                </Box>
                <Box>
                    <Button
                        sx={{
                            color: "inherit",
                            textTransform: "none",
                            fontSize: "18px",
                            mr: "35px",
                            "&:hover": { background: "transparent" },
                            "&& .MuiTouchRipple-child": { backgroundColor: "transparent" }
                        }}
                        startIcon={
                            <img
                                style={{ width: "30px", height: "30px" }}
                                src={like}
                                alt="likeIcon"
                            />
                        }
                    >
                        {likes}
                    </Button>
                    <Button
                        sx={{
                            color: "inherit",
                            textTransform: "none",
                            fontSize: "18px",
                            "&:hover": { background: "transparent" },
                            "&& .MuiTouchRipple-child": { backgroundColor: "transparent" }
                        }}
                        startIcon={
                            <img
                                style={{ width: "30px", height: "30px" }}
                                src={dislike}
                                alt="dislikeIcon"
                            />
                        }
                    >
                        {dislikes}
                    </Button>
                </Box>
            </Box>
        </Box>
    )
}

export default QuestionItem