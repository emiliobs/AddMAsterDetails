/// <reference path="jquery-3.1.0.min.js" />
/// <reference path="bootstrap.min.js" />
var Categories = [];
//fetch categories from database:
function LoadCategory(element)
{
    if (Categories.length == 0)
    {
        //ajax function for fetch data
        $.ajax
            ({

                type: 'GET',
                url: '/Home/GetProductCategories',
                success: function (data)
                { 
                    Categories = date;
                    //render category  
                    renderCategory(element);
                }

            });
    }
    else
    {
        //render category to the lement:
    }
};

function renderCategory(element)
{

    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('select'));
    $.each(Categories, function (i, val) {

        $ele.append($('<option/>').val(val.CategoryId).text(val.CategoryName));

    });

};

//fetch products
function LoadProduct(categoryDD)
{
    
};