//author: Kamran A-eff

(function ($) {

    $.makeid = function (length) {
        var result = '';
        var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        var charactersLength = characters.length;
        for (var i = 0; i < length; i++) {
            result += characters.charAt(Math.floor(Math.random() * charactersLength));
        }
        return result;
    };

    $.fn.imgadd = function (options) {
        let that = this;

        options = $.extend({
            tnWidth: '120px',
            tnHeight: '120px',
            fontSize: '55px',
            plusInnerHtml: '<i class="fa fa-image"></i>',
            viewerWidth: '300px',
            viewerHeight: '300px',
            readonly: false
        }, options);

        $(that).each(function (index, element) {

            let ix = 1;
            let guid = `i${$.makeid(8)}`;
            let viewer = $(element).find('.viewer');
            let viewerThumbs = $(element).find('.viewer-thumbs');
            let elName = $(element).attr('name');

            if (viewer.length == 0) {
                viewer = $(`<div/>`, {
                    class: 'viewer'
                })
                    .css({
                        'background-image': `url(${noImage})`
                    });

                $(element).append(viewer);
            }

            if (viewerThumbs.length == 0) {
                viewerThumbs = $(`<div/>`, {
                    class: 'viewer-thumbs',
                    name: 'image'
                });

                $(element).append(viewerThumbs);
            }

            $(element).find('label[thumb-url]').each(function (thumbIndex, thumb) {
                $(viewer).removeClass('no-show');

                let url = $(thumb).attr('thumb-url');
                let imageId = $(thumb).attr('image-id');
                let checked = $(thumb).attr('checked') || false;

                viewerThumbs.append(thumb);

                $(thumb).addClass('img-thumb')
                    .attr('for', `${guid}${ix}`)
                    .removeAttr('thumb-url')
                    .removeAttr('image-id')
                    .removeAttr('checked')
                    .css({
                        width: options.tnWidth,
                        height: options.tnHeight,
                        'background-image': `url(${url})`
                    });

                if (checked == true) {
                    $(thumb).addClass('active');
                }

                $(thumb).click(function () {
                    $(viewer).css({
                        'background-image': `url(${url})`
                    });

                    $(element).find('.img-thumb')
                        .removeClass('active')
                        .find('[name$=".IsMain"]')
                        .prop('checked', false);

                    $(this).addClass('active')
                        .find('[name$=".IsMain"]')
                        .prop('checked', true);
                });

                var fileName = url.substring((url.lastIndexOf('/') == 0 ? -1 : url.lastIndexOf('/')) + 1);

                $(thumb).append($(`
                <input type='radio' name='${elName}[${ix - 1}].IsMain'   id='${guid}${ix} ${(checked == `checked` ? `checked` : ``)}'/>
                <input type='hidden'   name='${elName}[${ix - 1}].TempPath' value='${fileName}'/>
                <input type='hidden'   name='${elName}[${ix - 1}].Id'       value='${imageId}'/>
                ${(options.readonly == true ? `` : `<span class='remove-thumb'></span>`)}`));

                $(thumb).find('[class$="remove-thumb"]')
                    .click(function (e) {
                        e.stopPropagation();
                        e.preventDefault();

                        $(this).closest('.img-thumb')
                            .addClass('d-none')
                            .removeClass('active')
                            .find('[name$=".TempPath"]').val('');

                        if ($(that).find('.img-thumb.active').length == 0) {
                            let founded = $(that).find(`.img-thumb:not([class*='d-none']):not([for='${$(this).closest('.img-thumb').attr('for')}'])`)
                                .eq(0);

                            if (founded.length > 0)
                                $(founded).trigger('click');
                            else {
                                $(viewer).css({
                                    'background-image': `url(${noImage})`
                                })
                                    .addClass('no-show');
                            }
                        }
                    });

                if (checked == `checked`)
                    $(thumb).trigger('click');

                ix++;
            });

            viewer.css({
                width: options.viewerWidth,
                height: options.viewerHeight
            });

            if (options.readonly == true)
                return;

            let btnPlus = $(`<button class='img-plus' type='button'>${options.plusInnerHtml}</button>`)
                .css({
                    width: options.tnWidth,
                    height: options.tnHeight,
                });

            if (options.plusBtnClass) {
                $(btnPlus).addClass(options.plusBtnClass);
            }
            else {
                $(btnPlus).css({
                    'font-size': options.fontSize
                })
                    .addClass('img-plus-no-bt');
            }


            $(btnPlus).click(function () {
                let fileInput = $(`<input name='${elName}[${ix - 1}].File' type="file" accept="image/x-png,image/gif,image/jpeg"/>`);

                let label = $(`<label for='${guid}${ix}' class='img-thumb' style="background-image:url('/libraries/multiple-image/img/img-rendering.gif')">
                                    <span class='remove-thumb'></span>
                               </label>`)
                    .append(fileInput)
                    .css({
                        width: options.tnWidth,
                        height: options.tnHeight
                    });

                $(element).find('.viewer-thumbs').append(label);

                $(label).append(`<input type='radio' name='${elName}[${ix - 1}].IsMain' id='${guid}${ix}'/>`);

                $(fileInput).change(function (e) {
                    $(label).attr('title', e.target.files[0].name);

                    let reader = new FileReader();
                    reader.addEventListener("load", function () {

                        $(label).css({
                            'background-image': `url(${reader.result})`
                        }).unbind('click')
                            .click(function () {

                                $(viewer).css({
                                    'background-image': `url(${reader.result})`
                                });

                                $(viewer).removeClass('no-show');

                                $(element).find('.img-thumb')
                                    .removeClass('active')
                                    .find('[name$=".IsMain"]')
                                    .prop('checked', false);

                                $(this).addClass('active')
                                    .find('[name$=".IsMain"]')
                                    .prop('checked', true);

                            });

                        $(label).find('[class$="remove-thumb"]')
                            .unbind('click')
                            .click(function (e) {
                                e.stopPropagation();
                                $(label).remove();

                                if ($(that).find('.img-thumb.active').length == 0) {
                                    let founded = $(that).find(`.img-thumb:not([class*='d-none']):not([for='${$(this).closest('.img-thumb').attr('for')}'])`)
                                        .eq(0);

                                    if (founded.length > 0)
                                        $(founded).trigger('click');
                                    else {
                                        $(viewer).css({
                                            'background-image': `url(${noImage})`
                                        });
                                    }
                                }
                            });

                        if ($(that).find('.img-thumb.active').length == 0)
                            $(label).trigger('click');

                    }, false);

                    reader.readAsDataURL(e.target.files[0]);
                });
                ix++;
                $(fileInput).trigger('click');
            });
            $(element).find('.viewer').append(btnPlus);
        });

    };


})(jQuery);