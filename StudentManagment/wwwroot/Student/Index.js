       

        $("#Add").on("click", function () {
           
            $.ajax({
                type: "POST",
                url: "/Home/CreateOrEditModal",
                data: { },
                success: function (response) {

                    $("#addEmployeeModal2").find('.modal-title').text('Add Student');
                    $("#addEmployeeModal2").find('.modal-body').html(response);
                    $("#addEmployeeModal2").modal('show');

                  
                   
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });

           

           
        });


        $(".edit").click(function () {
            var StudentId = this.id;
                $.ajax({
                type: "POST",
                url: "/Home/CreateOrEditModal",
                data: { "id": StudentId },
                success: function (response) {
                    $("#addEmployeeModal2").find('.modal-title').text('Edit Student');
                    $("#addEmployeeModal2").find('.modal-body').html(response);
                    $("#addEmployeeModal2").find('.modal-body').html(response);
                    $("#addEmployeeModal2").modal('show');

                   
                   
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
                });

           
        });


        $(".delete").on("click", function () {
            
            var Id = this.id;
           
                 $.ajax({
                        type: "GET",
                     url: "/Home/DeleteStudent?id=" + Id, 
                      
                      success: function (data) {
                    $("#StudentTable tbody").html(data);
                     },

                   failure: function (response) {
                  alert(response.responseText);
                  },
                 error: function (response) {
                alert(response.responseText);
                   }
});
          }); 
    

        $("#searchInput").on("input", function () {
            
            var searchText = $(this).val().toString();

            $.ajax({
                type: "GET",
                url: "/Home/GetStudentSearch", 
                data: { text: searchText },
                success: function (data) {

                    console.log(data)
                   
                    $("#StudentTable tbody").html(data);
                },
                
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });





    

       
       


      








  



