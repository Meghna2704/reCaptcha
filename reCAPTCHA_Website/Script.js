function LoginButton(){
    const reCaptchaResponse = grecaptcha.getResponse();
    if(reCaptchaResponse){
        $.ajax({
            type: "GET",
            url: "https://localhost:44317/api/User/Captcha",
            data: {userResponse : reCaptchaResponse},            
            success: function(data){
                if(data){
                    //API returned true
                    alert("Captcha Verified");
                }else{
                    //API returned false
                    alert("Please verify captcha again");
                }               
            },
            error: function(error){
                alert("Please try again");
            }
        });
    }
    else{
        alert("Something went wrong with reCaptcha. Please try again!");
    }
}